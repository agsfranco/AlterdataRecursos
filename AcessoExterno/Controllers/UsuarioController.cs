using System;
using System.Collections.Generic;
using System.Web.Http;
using DataAccess;
using Newtonsoft.Json;


namespace AcessoExterno
{
    public class UsuarioController : ApiController
    {
        // GET api/<controller>
        [Route("~/api/usuario/selecionarTodos")]
        [HttpGet]
        public object[] Get()
        {
            UsuarioData ud = new UsuarioData();
            try
            {
                return ud.GetUsuarioData();
            }
            finally
            {
                ud.Dispose();
            }
        }

        // GET api/<controller>/5
        [Route("~/api/usuario/selecionar")]
        [HttpGet]
        public object[] Get(int id)
        {
            UsuarioData ud = new UsuarioData();
            try
            {
                return ud.GetUsuarioData(id);
            }
            finally
            {
                ud.Dispose();
            }
        }

        //post api/<controller>
        [Route("~/api/usuario/insere")]
        [HttpPost]
        public int Post([FromBody]string value)
        {
            int resulte = -1;
            UsuarioData ud = new UsuarioData();
            try
            {
                if (value != null && value != "")
                {
                    usuario us = JsonConvert.DeserializeObject<usuario>(value);
                    resulte =  ud.InsereUsuario(us);
                }
            }
            finally
            {
                ud.Dispose();
            }
            return resulte;
        }
        
        // PUT api/<controller>/5
        [Route("~/api/usuario/altera")]
        [HttpPut]
        public bool Put(int id, [FromBody]string value)
        {
            bool resulte = false;
            //ignorar o id que ghega pelo int id. Gravar o id serializado.
            UsuarioData ud = new UsuarioData();
            try
            {
                if (value != null && value != "")
                {
                    usuario us = JsonConvert.DeserializeObject<usuario>(value);
                    resulte = ud.AlteraUsuario(us.id, us.nome, us.email, us.senha, Convert.ToBoolean(us.ativo));
                }
            }
            finally
            {
                ud.Dispose();
            }
            return resulte;
        }

        //DELETE api/<controller>/5
        [Route("~/api/usuario/delete/{id}")]
        [HttpDelete]
        public bool Delete(int id)
        {
            bool resulte = false;
            UsuarioData ud = new UsuarioData();
            try
            {
                resulte = ud.InativaUsuario(id);
            }
            finally
            {
                ud.Dispose();
            }
            return resulte;
        }
    }
}