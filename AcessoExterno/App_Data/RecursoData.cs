using DataAccess;
using System;
using System.Linq;

namespace AcessoExterno
{
    public class RecursoData
    {
        private AlterdataEntities _ae;

        public RecursoData()
        {
            _ae = new AlterdataEntities();
        }

        public object[] GetRecursoData()
        {
            var rec = (from l in _ae.recurso
                       select new
                       {
                           id = l.id,
                           titulo = l.titulo,
                           comentario = l.comentario,
                           data_cadastro = l.data_cadastro,
                           status = l.status
                       }).OrderBy(p => p.id).ToArray();
            return rec;
        }

        public object[] GetRecursoData(int Recurso_id)
        {
            var rec = (from l in _ae.recurso
                       where l.id == Recurso_id
                       select new
                       {
                           id = l.id,
                           titulo = l.titulo,
                           comentario = l.comentario,
                           data_cadastro = l.data_cadastro,
                           status = l.status
                       }).ToArray();
            return rec;
        }

        public int InsereRecurso(recurso rec)
        {
            int result = -1;
            int novoId = -1;

            try
            {
                novoId = (from l in _ae.recurso select l.id).Max() + 1;
            }
            catch//Tabela sem registros (primeiro insert)
            {
                novoId = 1;
            }
            finally
            {
                rec.id = novoId;                
                rec.data_cadastro = DateTime.Now;
                rec.status = 1; //sempre inserido com o primeiro status
                _ae.recurso.Add(rec);
                _ae.SaveChanges();
                result = rec.id;
            }

            return result;
        }

        public bool AlteraRecurso(int Recurso_id, string Titulo, string Comentario, int Status)
        {
            int result = 0;
            try
            {
                recurso rec = new recurso();
                rec = (from l in _ae.recurso
                       where l.id == Recurso_id
                       select l).First();
                rec.titulo = Titulo;
                rec.comentario = Comentario;
                rec.status = Status;
                result = _ae.SaveChanges();
            }
            catch { }
            return result > 0;
        }

        public bool CancelaRecurso(int Recurso_id)
        {
            int result = 0;
            try
            {
                recurso rec = new recurso();
                rec = (from l in _ae.recurso
                       where l.id == Recurso_id
                       select l).First();
                rec.status = 4; //Cancelado
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