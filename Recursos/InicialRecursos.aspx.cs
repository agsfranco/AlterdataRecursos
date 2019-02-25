using System;
using System.Web.UI.WebControls;

namespace Recursos
{
    public partial class InicialRecursos : SecurePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ClientScript.RegisterClientScriptInclude("ScriptInicialRecursos", "/js/ScriptInicialRecursos.js");
                try
                {
                    GetData((int)Session[Constantes.UsuarioId]);
                }
                catch
                {

                }
            }
            else
            {
                
            }
        }

        private void GetData(int Usuario_id)
        {
            RecursosData rd = new RecursosData();
            try
            {
                GridRecursos.DataSource = rd.GetRecursosData(Usuario_id);
                GridRecursos.DataBind();
                UpdatePanelRecursos.Update();
            }
            finally
            {
                rd.Dispose();
            }

        }

        private void GetVotacoesData(int Recurso_id)
        {
            RecursosData rd = new RecursosData();            
            try
            {
                GridVotos.DataSource = rd.GetVotosData(Recurso_id);
                GridVotos.DataBind();
                lblTituloVotacoes.Text = rd.GetTituloRecurso(Recurso_id);
            }
            finally
            {
                rd.Dispose();
            }
        }

        private void VisualizarVotacoes(bool Exibir, int Recurso_id = -1)
        {
            if (Exibir == true)
            {
                HfExibirVotos.Value = Recurso_id.ToString();
                GetVotacoesData(Recurso_id);
                PanelVisualizarVotacoes.Visible = true;
                UpdatePanelVotações.Update();
            }
            else
            {
                HfExibirVotos.Value = "";
                PanelVisualizarVotacoes.Visible = false;
                UpdatePanelVotações.Update();
            }
        }

        private void VisualizarVotar(bool Exibir, int Recurso_id = -1)
        {
            if (Exibir == true)
            {
                HfVotar.Value = Recurso_id.ToString();
                RecursosData rd = new RecursosData();
                try
                {
                    lblTituloComentarioRecurso.Text = rd.GetTituloRecurso(Recurso_id);
                }
                catch { }
                finally
                {
                    rd.Dispose();
                }
                PanelVotarRecurso.Visible = true;
                UpdatePanelVotarRecurso.Update();
            }
            else
            {
                HfVotar.Value = "";
                lblTituloComentarioRecurso.Text = "";
                tbComentarioVoto.Text = "";
                PanelVotarRecurso.Visible = false;
                UpdatePanelVotarRecurso.Update();
            }
        }

        protected void GridRecursos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = -1;
            RecursosData rd = new RecursosData();
            try
            {
                switch (e.CommandName)
                {
                    case "VerVotos":
                        id = Convert.ToInt32(e.CommandArgument.ToString());
                        VisualizarVotar(false);
                        VisualizarVotacoes(true,id);
                        break;
                    case "Votar":
                        id = Convert.ToInt32(e.CommandArgument.ToString());
                        VisualizarVotar(true, id);
                        VisualizarVotacoes(false);
                        break;
                }
            }
            catch { }
            finally
            {
                rd.Dispose();
            }
        }

        protected void GridRecursos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowIndex >= 0)
            {
                //e.Row.Cells[4] -> Status
                //e.Row.Cells[5] -> Total de votos
                //e.Row.Cells[6] -> Qtde Votos Usuario Atual
                //e.Row.Cells[7] -> Ver Votacao
                //e.Row.Cells[8] -> Votar

                //Só visualiza se houverem votos para ser visualizados.
                if ((e.Row.Cells[5].Text != "") && (Convert.ToInt32(e.Row.Cells[5].Text) <= 0)) { e.Row.Cells[7].Text = ""; }

                //Se o usuário já votou, não pode votar novamente.
                if ((e.Row.Cells[6].Text != "") && (Convert.ToInt32(e.Row.Cells[6].Text) > 0)) { e.Row.Cells[8].Text = ""; }
                
                /*Status dos recursos: 1-proposto 2-em_desenvolvimento 3-concluido 4-cancelado*/
                //So é possível votar com o status de proposto.
                if ((e.Row.Cells[4].Text != "") && (Convert.ToInt32(e.Row.Cells[4].Text) > 1)) { e.Row.Cells[8].Text = ""; }

                //Torna os status mais legíveis.
                switch (Convert.ToInt32(e.Row.Cells[4].Text))
                {
                    case (int)Constantes.StatusVotacao.proposto:
                        e.Row.Cells[4].Text = "1-Proposto";
                    break;
                    case (int)Constantes.StatusVotacao.em_desenvolvimento:
                        e.Row.Cells[4].Text = "2-Desenvolvimento";
                        break;
                    case (int)Constantes.StatusVotacao.concluido:
                        e.Row.Cells[4].Text = "3-Concluído";
                        e.Row.Cells[8].Text = "";
                        break;
                    case (int)Constantes.StatusVotacao.cancelado:
                        e.Row.Cells[4].Text = "4-Cancelado";
                        e.Row.Cells[8].Text = "";
                        break; 
                }
            }
        }

        private static DateTime FromUnixTime(long UnixTime)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return epoch.AddMilliseconds(UnixTime);
        }

        protected void btnVotar_Click(object sender, EventArgs e)
        {
            RecursosData rd = new RecursosData();
            try
            {
                rd.GravaVoto(Convert.ToInt32(Session[Constantes.UsuarioId]), Convert.ToInt32(HfVotar.Value), tbComentarioVoto.Text, FromUnixTime(Convert.ToInt64(HfDateTimeCliente.Value)));
            }
            finally
            {
                VisualizarVotar(false);
                GetData(Convert.ToInt32(Session[Constantes.UsuarioId]));
                rd.Dispose();
            }
        }

        protected void btnCancelarVoto_Click(object sender, EventArgs e)
        {
            VisualizarVotar(false);
            GetData(Convert.ToInt32(Session[Constantes.UsuarioId]));
        }

        protected void btnlVisualizarVotacoesFechar_Click(object sender, EventArgs e)
        {
            VisualizarVotacoes(false);
        }
    }
}