using DataAccess;
using System.Linq;
using System;

namespace Recursos
{
    public class RecursosData
    {
        private AlterdataEntities _ae;

        public RecursosData()
        {
            _ae = new AlterdataEntities();
        }

        public int GravaVoto(int Usuario_id, int Recurso_id, string Comentario, DateTime DataHoraCliente)
        {
            int result = -1;
            int novoId = -1;

            votacao vot = new votacao();
            try
            {
                novoId = (from l in _ae.votacao select l.id).Max() + 1;
            }
            catch//Tabela sem registros (primeiro insert)
            {
                novoId = 1;
            }
            finally
            {
                vot.id = novoId;
                vot.usuario_id = Usuario_id;
                vot.recurso_id = Recurso_id;
                vot.comentario = Comentario;
                vot.data_local = DateTime.Now;
                vot.data_remota = DataHoraCliente;
                _ae.votacao.Add(vot);
                _ae.SaveChanges();
                result = vot.id;
            }
            return result;
        }

        public object GetRecursosData(int Usuario_id)
        {
            var recurso = (from rec in _ae.recurso
                           select new
                           {
                               rec.id,
                               rec.titulo,
                               rec.comentario,
                               rec.data_cadastro,
                               rec.status,
                               qtde_votos = rec.votacao.Count(),
                               votos_usr_atual = (from vot in _ae.votacao where (vot.usuario_id == Usuario_id && vot.recurso_id == rec.id) select vot).Count()
                           }
                           ).OrderByDescending(p => p.qtde_votos).ToList();
            return recurso;
        }

        public object GetVotosData(int Recurso_id)
        {
            var recurso = (from rec in _ae.votacao
                           where rec.recurso_id == Recurso_id
                           select new
                           {
                               rec.id,
                               rec.comentario,
                               rec.data_local,
                               rec.data_remota,
                               usr = rec.usuario.id + " - " + rec.usuario.nome
                           }
                           ).OrderByDescending(p => p.data_local).ToList();
            return recurso;
        }

        public string GetTituloRecurso(int Recurso_id)
        {

            string resulte = (from t in _ae.recurso
                              where t.id == Recurso_id
                              select t.id + " - " + t.titulo).First();
            return resulte;
        }

        public void Dispose()
        {
            _ae.Dispose();
        }
    }
}