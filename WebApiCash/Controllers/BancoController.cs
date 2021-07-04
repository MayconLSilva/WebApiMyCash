using Newtonsoft.Json.Converters;
using Swashbuckle.Examples;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace WebApiCash
{
    [RoutePrefix("api/banco")]
    public class BancoController : ApiController
    {
        private ApplicationDbContext _context = new ApplicationDbContext();

        Util util = new Util();

        /// <summary>Método GET - Lista todos bancos do usuário</summary>   
        /// <param name="value">Objeto código do usuário</param>
        /// <returns>teste retorno</returns>
        ///<remarks> Utilize este método quando desejar listar os bancos do usuário, deverá passara id do usuário! </remarks> 
        /// <response code="400">Algum parâmetro passado está incorreto!</response>
        /// <response code="200">Lista retornada com sucesso!</response>
        /// <response code="404">Nenhum valor encontrado!</response>        
        [Route("buscaBancos")]
        [HttpGet]        
        public IHttpActionResult GetBancos(string value)
        {
            if (string.IsNullOrEmpty(value))
                return BadRequest("O atributo do usuário deve ser informado");

            var bancos = _context.Bancos.SqlQuery($"select *from banco where idUsuario_fk = {value}").ToList().FirstOrDefault();             

            if (bancos == null)
                return NotFound();

            var ativoUsuario = util.validaUsuario(Convert.ToInt32(value));
            if (ativoUsuario != "1")
                return BadRequest("Usuário não está ativo!");

            return Ok(bancos);
        }

        /// <summary>Método POST - Insere banco</summary>   
        /// <returns>teste retorno</returns>
        ///<remarks> Utilize este método quando desejar inserir o banco do usuário, deverá passar todos os campos! </remarks> 
        /// <response code="400">Algum parâmetro passado está incorreto!</response>
        [SwaggerResponse(HttpStatusCode.Created, Type = typeof(Banco), Description = "Banco inserido com sucesso!")]
        [SwaggerResponseExample(HttpStatusCode.Created, typeof(BancoResponseExamplePost), jsonConverter: typeof(StringEnumConverter))]
        [Route("inserirBancos")]
        [HttpPost]
        public IHttpActionResult PostBancos(Banco banco)
        {

            if (banco.idUsuario_fk == 0 || string.IsNullOrEmpty(banco.idUsuario_fk.ToString()))
                return BadRequest("É necessário informar a id do usuário");

            if (banco.nomeBanco.Length > 45)
                return BadRequest("Banco excedeu o limite máximo de caracteres!");

            var ativoUsuario = util.validaUsuario(Convert.ToInt32(banco.idUsuario_fk));
            if (ativoUsuario != "1")
                return BadRequest("Usuário não está ativo!");

            _context.Entry(banco).State = System.Data.Entity.EntityState.Added;
            _context.SaveChanges();

            return CreatedAtRoute("", new { id = banco.idBanco }, banco.idBanco);
        }

        /// <summary>Método PUT - Atualiza banco</summary>        
        ///<remarks> Utilize este método quando desejar atualizar o banco do usuário, deverá passar todos os campos, independente de qual informação deseje atualizar! </remarks> 
        /// <response code="400">Algum parâmetro passado está incorreto!</response> 
        /// <response code="404">Nenhum valor encontrado!</response>   
        [SwaggerResponse(HttpStatusCode.NoContent, Type = typeof(Banco), Description = "Banco atualizado com sucesso!")]
        [SwaggerResponseExample(HttpStatusCode.NoContent, typeof(BancoResponseExamplePut), jsonConverter: typeof(StringEnumConverter))]       
        [Route("atualizaBancos")]
        [HttpPut]
        public IHttpActionResult PutBancos(int id, Banco banco)
        {
            if (!ModelState.IsValid)
                return BadRequest("Não é um modelo válido, verifique os métodos passado");

            if (_context.Bancos.Count(x => x.idBanco == id) == 0)
            {
                return NotFound();
            }

            if (banco.idUsuario_fk != 0)
                return BadRequest("Não é necessário informar a id do usuário!");

            //Verifico métodos passado
            if (string.IsNullOrEmpty(banco.nomeBanco))
                return BadRequest("Verifique os métodos passado!");

            //Atribuo o código do usário
            var user = _context.Database.SqlQuery<string>($"select idUsuario_fk from banco where idBanco = {id}").FirstOrDefault();
            banco.idUsuario_fk = Convert.ToInt32(user);
            banco.idBanco = id;

            var ativoUsuario = util.validaUsuario(Convert.ToInt32(user));
            if (ativoUsuario != "1")
                return BadRequest("Usuário não está ativo!");

            _context.Entry(banco).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>Método DELETE - Deleta banco</summary>   
        /// <returns>teste retorno</returns>
        ///<remarks> Utilize este método quando desejar deletar um banco do usuário!</remarks> 
        /// <response code="400">Algum parâmetro passado está incorreto!</response>
        /// <response code="404">Registro informado não encontrado!</response>
        [SwaggerRequestExample(typeof(Banco), typeof(BancoResponseExamplePut), jsonConverter: typeof(StringEnumConverter))]
        [Route("deletarBancos")]
        [HttpDelete]
        public IHttpActionResult DeleteBancos(int id)
        {
            if (_context.Bancos.Count(x => x.idBanco == id) == 0)
            {
                return NotFound();
            }

            var banco = new Banco
            {
                idBanco = id
            };

            _context.Entry(banco).State = System.Data.Entity.EntityState.Deleted;
            _context.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
