using System;
using System.Web.Http;
using DataAccess;
using Newtonsoft.Json;

namespace AcessoExterno
{
    public class RecursoController : ApiController
    {
        // GET api/<controller>
        [Route("~/api/recurso/selecionarTodos")]
        [HttpGet]
        public object[] Get()
        {
            RecursoData ud = new RecursoData();
            try
            {
                return ud.GetRecursoData();
            }
            finally
            {
                ud.Dispose();
            }
        }

        // GET api/<controller>/5
        [Route("~/api/recurso/selecionar")]
        [HttpGet]
        public object[] Get(int id)
        {
            RecursoData ud = new RecursoData();
            try
            {
                return ud.GetRecursoData(id);
            }
            finally
            {
                ud.Dispose();
            }
        }

        //post api/<controller>
        [Route("~/api/recurso/insere")]
        [HttpPost]
        public int Post([FromBody]string value)
        {
            int resulte = -1;
            RecursoData ud = new RecursoData();
            try
            {
                if (value != null && value != "")
                {
                    recurso us = JsonConvert.DeserializeObject<recurso>(value);
                    resulte = ud.InsereRecurso(us);
                }
            }
            finally
            {
                ud.Dispose();
            }
            return resulte;
        }

        // PUT api/<controller>/5
        [Route("~/api/recurso/altera")]
        [HttpPut]
        public bool Put(int id, [FromBody]string value)
        {
            bool resulte = false;
            //ignorar o id que ghega pelo int id. Mandar o id serializado.
            RecursoData ud = new RecursoData();
            try
            {
                if (value != null && value != "")
                {
                    recurso us = JsonConvert.DeserializeObject<recurso>(value);
                    resulte = ud.AlteraRecurso(us.id, us.titulo, us.comentario, Convert.ToInt32(us.status));
                }
            }
            finally
            {
                ud.Dispose();
            }
            return resulte;
        }

        //DELETE api/<controller>/5
        [Route("~/api/recurso/delete/{id}")]
        [HttpDelete]
        public bool Delete(int id)
        {
            bool resulte = false;
            RecursoData ud = new RecursoData();
            try
            {
                resulte = ud.CancelaRecurso(id);
            }
            finally
            {
                ud.Dispose();
            }
            return resulte;
        }
    }
}