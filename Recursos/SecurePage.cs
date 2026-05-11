using System;
using System.Web.UI;

namespace Recursos
{
    public class SecurePage : Page
    {
        public bool VerificaLogado()
        {
            bool logado = false;
            LoginData rd = new LoginData();
            try
            {
                logado = (Session[Constantes.UsuarioId] != null || (int)Session[Constantes.UsuarioId] > 0) &&//existe o id do logado na sessao.
                         (rd.VerificaLogon((int)Session[Constantes.UsuarioId],Session.SessionID)); //Verifica na base se o login esta dentro do tempo de vida.                  
            }
            catch { }
            finally
            {
                rd.Dispose();
            }
            
            return logado;
        }

        public bool RenovaLogado(int Usuario_id, string Sessao_id)
        {
            bool logado = false;
            LoginData rd = new LoginData();
            try
            {
                rd.RenovaLogon(Usuario_id, Sessao_id);
            }
            finally
            {
                rd.Dispose();
            }

            return logado;
        }

        private bool GravaLogoff(int Usuario_id, string Sessao_id)
        {
            bool logado = false;
            LoginData rd = new LoginData();
            try
            {
                rd.RenovaLogon(Usuario_id,Sessao_id);
            }
            finally
            {
                rd.Dispose();
            }

            return logado;
        }

        public void GravaSessaoLogin(int UsuarioId,string UsuarioNome)
        {
            Session[Constantes.LogadoStatus] = Constantes.StatusLogin.logado;
            Session[Constantes.UsuarioId] = UsuarioId;
            Session[Constantes.UsuarioNome] = UsuarioNome;
        }

        public void LimpaSessaoLogin()
        {
            Session[Constantes.LogadoStatus] = null;
            Session[Constantes.UsuarioId] = null;
            Session[Constantes.UsuarioNome] = null;
        }

        protected override void OnLoad(EventArgs e)
        {
            //if (IsPostBack)
            //{                
                if (VerificaLogado() == true)//Verifica se está logado e ou renova o ttl ou redireciona para pagina de login.
                {
                    //O login esta correto. Renova o login
                    RenovaLogado((int)Session[Constantes.UsuarioId],Session.SessionID);
                }
                else
                {
                    if ((Session[Constantes.UsuarioId] !=null) && ((int)Session[Constantes.UsuarioId] > 0))
                    {//garante a integridade da base (limpa possíveis logons antigos expirados ou nao)
                        GravaLogoff((int)Session[Constantes.UsuarioId],Session.SessionID);
                    }
                    LimpaSessaoLogin();
                    Session[Constantes.PaginaRequisitada] = "";
                    Response.Redirect(Constantes.UrlLogin);
                }  
            //}
            base.OnLoad(e);
        }
    }
}