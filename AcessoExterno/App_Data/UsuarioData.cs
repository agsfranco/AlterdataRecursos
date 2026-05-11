using DataAccess;
using System;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace AcessoExterno
{
    public class UsuarioData
    {
        private AlterdataEntities _ae;

        public UsuarioData()
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

        public object[] GetUsuarioData()
        {
            var usr = (from l in _ae.usuario
                       select new
                       {
                           id = l.id,
                           nome = l.nome,
                           email = l.email,
                           data_cadastro = l.data_cadastro,
                           ativo = l.ativo
                       }).OrderBy(p => p.id).ToArray();
            return usr;
        }

        public object[] GetUsuarioData(int Usuario_id)
        {
            var usr = (from l in _ae.usuario
                       where l.id == Usuario_id
                       select new
                       {
                           id = l.id,
                           nome = l.nome,
                           email = l.email,
                           data_cadastro = l.data_cadastro,
                           ativo = l.ativo
                       }).ToArray();
            return usr;
        }

        public int InsereUsuario(usuario usr)
        {
            int result = -1;
            int novoId = -1;

            try
            {
                novoId = (from l in _ae.usuario select l.id).Max() + 1;
            }
            catch//Tabela sem registros (primeiro insert)
            {
                novoId = 1;
            }
            finally
            {
                usr.id = novoId;
                usr.senha = CryptSenha(usr.senha);
                usr.data_cadastro = DateTime.Now;
                usr.ativo = true;//Na inserção sempre ativo
                _ae.usuario.Add(usr);
                _ae.SaveChanges();
                result = usr.id;
            }

            return result;
        }

        public bool AlteraUsuario(int Usuario_id, string Nome, string Email, string Senha, bool Ativo)
        {
            int result = 0;
            Senha = CryptSenha(Senha);
            try
            {
                usuario usr = new usuario();
                usr = (from l in _ae.usuario
                        where l.id == Usuario_id
                        select l).First();
                usr.nome = Nome;
                usr.email = Email;
                usr.senha = Senha;
                usr.ativo = Ativo;
                result = _ae.SaveChanges();
            }
            catch { }
            return result > 0;
        }

        public bool InativaUsuario(int Usuario_id)
        {
            int result = 0;
            try
            {
                usuario usr = new usuario();
                usr = (from l in _ae.usuario
                       where l.id == Usuario_id
                       select l).First();
                usr.ativo = false;
                result = _ae.SaveChanges();
            }
            catch { }
            return result > 0;
        }

        public void Dispose()
        {
            _ae.Dispose();
        }
    }
}