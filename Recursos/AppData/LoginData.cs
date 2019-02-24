using DataAccess;
using System;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Recursos
{
    public class LoginData
    {
        private AlterdataEntities _ae;

        public LoginData()
        {
            _ae = new AlterdataEntities();
        }

        public static string CryptSenha(string Value)
        {
            UnicodeEncoding encoding = new UnicodeEncoding();
            byte[] hashBytes;
            using (HashAlgorithm hash = SHA1.Create())
                hashBytes = hash.ComputeHash(encoding.GetBytes(Value));

            StringBuilder hashValue = new StringBuilder(hashBytes.Length * 2);
            foreach (byte b in hashBytes)
            {
                hashValue.AppendFormat(CultureInfo.InvariantCulture, "{0:X2}", b);
            }

            return hashValue.ToString();
        }

        public usuario VerificaUsuario(string Email, string Senha)
        {
            Senha = CryptSenha(Senha);
            try
            {
                usuario usr = (from l in _ae.usuario
                               where l.ativo == true && l.email == Email && l.senha == Senha
                               select l).First();
                return usr;
            }
            catch
            {
                usuario usr = null;
                return usr;
            }

        }

        public int GravaLogon(int Usuario_id, string Sessao_id)
        {
            int result = -1;
            int novoId = -1;

            login log = new login();
            try
            {
                novoId = (from l in _ae.login select l.id).Max() + 1;
            }
            catch//Tabela sem registros (primeiro insert)
            {
                novoId = 1;
            }
            finally
            {
                log.id = novoId;
                log.usuario_id = Usuario_id;
                log.sessao_id = Sessao_id;
                log.data_acesso = DateTime.Now;
                log.ttl = DateTime.Now.AddMinutes(Constantes.LoginTtl);
                _ae.login.Add(log);
                _ae.SaveChanges();
                result = log.id;
            }

            return result;
        }

        public bool GravaLogoff(int Usuario_id) //Não considerei a sessao para limpar todos os logins do usuario. Se ele sair em um navegador sai em todos.
        {
            int result = 0;
            try
            {
                var log = (from l in _ae.login
                           where l.usuario_id == Usuario_id
                           select l).ToList();

                foreach (login l in log) //Corrige possíveis erros.
                {
                    _ae.login.Remove(l);
                    result = result + _ae.SaveChanges();
                }
            }
            catch { }
            return result > 0;
        }

        public bool RenovaLogon(int Usuario_id, string Sessao_id)
        {
            int result = 0;
            try
            {
                login log = new login();
                log = (from l in _ae.login
                       where l.usuario_id == Usuario_id && l.sessao_id == Sessao_id
                       select l).First();
                log.data_acesso = DateTime.Now;
                log.ttl = DateTime.Now.AddMinutes(Constantes.LoginTtl);
                result = _ae.SaveChanges();
            }
            catch { }
            return result > 0;
        }

        public bool VerificaLogon(int Usuario_id, string Sessao_id)
        {
            bool result = false;
            try
            {
                login log = new login();
                log = (from l in _ae.login
                       where l.usuario_id == Usuario_id && l.sessao_id == Sessao_id
                       select l).OrderByDescending(p => p.id).First();

                result = (log.ttl > DateTime.Now);
            }
            catch { }

            return result;
        }

        public void Dispose()
        {
            _ae.Dispose();
        }
    }
}