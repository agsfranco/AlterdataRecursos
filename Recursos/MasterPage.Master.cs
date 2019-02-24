using System;

namespace Recursos
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            PanelLogin.Visible = false;
            if ((Session[Constantes.LogadoStatus] != null) &&
               ((Constantes.StatusLogin)Session[Constantes.LogadoStatus] == Constantes.StatusLogin.logado))
            {//Não verifica na base porque a SecurePage já faz isso. É apenas para exibição do painel de login.
                PanelLogin.Visible = true;
                lblId.Text = Session[Constantes.UsuarioId].ToString();
                lblNome.Text = Session[Constantes.UsuarioNome].ToString();
            }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            LoginData rd = new LoginData();
            if (rd.GravaLogoff((int)Session[Constantes.UsuarioId]))
            {
                Session[Constantes.LogadoStatus] = null;
                Session[Constantes.UsuarioId] = null;
                Session[Constantes.UsuarioNome] = null;
                Response.Redirect(Constantes.UrlLogin);
            }
        }
    }
}