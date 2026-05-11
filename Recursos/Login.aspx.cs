using System;
using System.Web.UI;
using DataAccess;


namespace Recursos
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Session[Constantes.LogadoStatus] != null) && 
                ((Constantes.StatusLogin)Session[Constantes.LogadoStatus] == Constantes.StatusLogin.logado))
            {  //Corrige algum redirecionamento incorreto.
                Response.Redirect(Constantes.PaginaInicial);
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {           
            LoginData ld = new LoginData();
            usuario usr = new usuario();
            usr = ld.VerificaUsuario(tbEmail.Text, tbSenha.Text);
            if (usr != null)//Email e senha corretos.
            {
                if (ld.GravaLogon(usr.id,Session.SessionID) > 0)//O login foi gravado
                {
                    Session[Constantes.LogadoStatus] = Constantes.StatusLogin.logado;
                    Session[Constantes.UsuarioId] = usr.id;
                    Session[Constantes.UsuarioNome] = usr.nome;

                    Response.Redirect(Constantes.PaginaInicial);
                }
            }//Senao retorna ao login.
            else
            {
                lblMsgErro.Visible = true;
            }
        }
    }
}