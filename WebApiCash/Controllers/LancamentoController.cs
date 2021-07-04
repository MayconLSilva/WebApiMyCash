using Newtonsoft.Json.Converters;
using Swashbuckle.Examples;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApiCash.Controllers
{
    [RoutePrefix("api/lancamentos")]
    public class LancamentoController : ApiController
    {
        private ApplicationDbContext _context = new ApplicationDbContext();

        /// <summary>Método GET - Lista todos lançamentos bancários do usuário</summary>   
        /// <param name="value">Objeto código do usuário</param>
        /// <returns>teste retorno</returns>
        ///<remarks> Utilize este método quando desejar listar os lançamentos bancários do usuário, deverá passara id do usuário! </remarks> 
        /// <response code="400">Algum parâmetro passado está incorreto!</response>
        /// <response code="200">Lista retornada com sucesso!</response>
        /// <response code="404">Nenhum valor encontrado!</response>
        [Route("buscaLancamentos")]
        [HttpGet]
        public IHttpActionResult GetLancamentos(string value)
        {
            var lancamentos = _context.LancamentosBanc.SqlQuery($"select *from lanbancario where idUsuario_fk = {value}").ToList();

            if (lancamentos == null)
                return NotFound();

            return Ok(lancamentos);
        }

        /// <summary>Método POST - Insere lançamento bancário</summary>   
        /// <returns>teste retorno</returns>
        ///<remarks> Utilize este método quando desejar inserir um lançamento bancário do usuário, deverá passar todos os campos! </remarks> 
        /// <response code="400">Algum parâmetro passado está incorreto!</response>
        [SwaggerResponse(HttpStatusCode.Created, Type = typeof(LancamentoBancario), Description = "Lançamento bancário inserido com sucesso!")]
        [SwaggerResponseExample(HttpStatusCode.Created, typeof(LancamentoResponseExamplePost), jsonConverter: typeof(StringEnumConverter))]
        [Route("inserirLancamentos")]
        [HttpPost]
        public IHttpActionResult PostLancamentos(LancamentoBancario lancamentoBancario)
        {

            if (lancamentoBancario.idUsuario_fk == 0 || string.IsNullOrEmpty(lancamentoBancario.idUsuario_fk.ToString()))
                return BadRequest("É necessário informar a id do usuário");

            if(lancamentoBancario.bancoLanBancario == 0 || lancamentoBancario.dataSaidaLanBancario.ToString() == "01/01/0001 00:00:00" || lancamentoBancario.valorLanBancario == 0 || lancamentoBancario.tipoLanBancario == "")
                return BadRequest("Verifique os parâmetros passado!");

            var idBanco = _context.Database.SqlQuery<string>($"select *from banco where idBanco = {lancamentoBancario.bancoLanBancario} and idUsuario_fk = {lancamentoBancario.idUsuario_fk}").FirstOrDefault();
            if (idBanco == "")
                return BadRequest("Id do banco informada não encontrada");


            _context.Entry(lancamentoBancario).State = System.Data.Entity.EntityState.Added;
            _context.SaveChanges();

            return CreatedAtRoute("", new { id = lancamentoBancario.idLanBancario }, lancamentoBancario.idLanBancario);
        }

        /// <summary>Método PUT - Atualiza laçamento bancário</summary>        
        ///<remarks> Utilize este método quando desejar atualizar um lançamento bancário do usuário, deverá passar todos os campos, independente de qual informação deseje atualizar! </remarks> 
        /// <response code="400">Algum parâmetro passado está incorreto!</response> 
        /// <response code="404">Nenhum valor encontrado!</response>   
        [SwaggerResponse(HttpStatusCode.NoContent, Type = typeof(LancamentoBancario), Description = "Lançamento bancário atualizado com sucesso!")]
        [SwaggerResponseExample(HttpStatusCode.NoContent, typeof(LancamentoResponseExamplePut), jsonConverter: typeof(StringEnumConverter))]
        [Route("atualizaLancamentos")]
        [HttpPut]
        public IHttpActionResult PutLancamentos(int id, LancamentoBancario lancamentoBancario)
        {
            if (!ModelState.IsValid)
                return BadRequest("Não é um modelo válido, verifique os métodos passado");

            if (_context.LancamentosBanc.Count(x => x.idLanBancario == id) == 0)
            {
                return NotFound();
            }

            if (lancamentoBancario.idUsuario_fk != 0)
                return BadRequest("Não é necessário informar a id do usuário!");

            var idBanco = _context.Database.SqlQuery<string>($"select *from banco where idBanco = {lancamentoBancario.bancoLanBancario} and idUsuario_fk = {lancamentoBancario.idUsuario_fk}").FirstOrDefault();
            if (idBanco == "")
                return BadRequest("Id do banco informada não encontrada");

            if (lancamentoBancario.bancoLanBancario == 0 || lancamentoBancario.dataSaidaLanBancario.ToString() == "01/01/0001 00:00:00" || lancamentoBancario.valorLanBancario == 0 || lancamentoBancario.tipoLanBancario == "")
                return BadRequest("Verifique os parâmetros passado!");

            //Atribuo o código do usário
            var user = _context.Database.SqlQuery<string>($"select idUsuario_fk from lanbancario where idLanBancario = {id}").FirstOrDefault();
            lancamentoBancario.idUsuario_fk = Convert.ToInt32(user);
            lancamentoBancario.idLanBancario = id;

            _context.Entry(lancamentoBancario).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>Método DELETE - Deleta lançamento bancário</summary>   
        /// <returns>teste retorno</returns>
        ///<remarks> Utilize este método quando desejar deletar um lançamento bancário do usuário!</remarks> 
        /// <response code="400">Algum parâmetro passado está incorreto!</response>
        /// <response code="404">Registro informado não encontrado!</response>
        [Route("deletarLancamentos")]
        [HttpDelete]
        public IHttpActionResult DeleteLancamentos(int id)
        {
            if (_context.LancamentosBanc.Count(x => x.idLanBancario == id) == 0)
            {
                return NotFound();
            }

            var lancamentoBancario = new LancamentoBancario
            {
                idLanBancario = id
            };

            _context.Entry(lancamentoBancario).State = System.Data.Entity.EntityState.Deleted;
            _context.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

    }
}
