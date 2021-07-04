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
    [RoutePrefix("api/debito")]
    public class DebitoController : ApiController
    {
        private ApplicationDbContext _context = new ApplicationDbContext();

        /// <summary>Método GET - Lista todas as contas/debito do usuário</summary>   
        /// <param name="value">Objeto código do usuário</param>
        /// <returns>teste retorno</returns>
        ///<remarks> Utilize este método quando desejar listar as contas/debito do usuário, deverá passara id do usuário! </remarks> 
        /// <response code="400">Algum parâmetro passado está incorreto!</response>
        /// <response code="200">Lista retornada com sucesso!</response>
        /// <response code="404">Nenhum valor encontrado!</response>
        [Route("buscaDebitos")]
        [HttpGet]
        public IHttpActionResult GetDebitos(string value)
        {
            var debitos = _context.Debitos.SqlQuery($"select *from debito where idUsuario_fk = {value}").ToList();

            if (debitos == null)
                return NotFound();

            return Ok(debitos);
        }

        /// <summary>Método POST - Insere contas/débito do usuário</summary>   
        /// <returns>teste retorno</returns>
        ///<remarks> Utilize este método quando desejar inserir uma despesa do usuário, deverá passar todos os campos! </remarks> 
        /// <response code="400">Algum parâmetro passado está incorreto!</response>
        [SwaggerResponse(HttpStatusCode.Created, Type = typeof(Debito), Description = "Conta/débito inserida com sucesso!")]
        [SwaggerResponseExample(HttpStatusCode.Created, typeof(DebitoResponseExamplePost), jsonConverter: typeof(StringEnumConverter))]
        [Route("inserirDebitos")]
        [HttpPost]
        public IHttpActionResult PostDebitos(Debito debito)
        {

            if (debito.idUsuario_fk == 0 || string.IsNullOrEmpty(debito.idUsuario_fk.ToString()))
                return BadRequest("É necessário informar a id do usuário");

            if(string.IsNullOrEmpty(debito.tipoDocumentoDebito) || debito.dataLancamentoDebito.ToString() == "01/01/0001 00:00:00" || debito.dataVencimentoDebito.ToString() == "01/01/0001 00:00:00" || debito.valorDebito == 0 || string.IsNullOrEmpty(debito.categoriaDebito))
                return BadRequest("Verifique os métodos passado");            

            _context.Entry(debito).State = System.Data.Entity.EntityState.Added;
            _context.SaveChanges();

            return CreatedAtRoute("", new { id = debito.idDebito }, debito.idDebito);
        }

        /// <summary>Método PUT - Atualiza conta/débito usuário</summary>        
        ///<remarks> Utilize este método quando desejar atualizar uma conta/débito do usuário, deverá passar todos os campos, independente de qual informação deseje atualizar! </remarks> 
        /// <response code="400">Algum parâmetro passado está incorreto!</response> 
        /// <response code="404">Nenhum valor encontrado!</response> 
        [SwaggerResponse(HttpStatusCode.NoContent, Type = typeof(Debito), Description = "Conta/débito atualizada com sucesso!")]
        [SwaggerResponseExample(HttpStatusCode.NoContent, typeof(DebitoResponseExamplePut), jsonConverter: typeof(StringEnumConverter))]
        [Route("atualizaDebitos")]
        [HttpPut]
        public IHttpActionResult PutDebitos(int id, Debito debito)
        {
            if (!ModelState.IsValid)
                return BadRequest("Não é um modelo válido, verifique os métodos passado");

            if (_context.Debitos.Count(x => x.idDebito == id) == 0)
            {
                return NotFound();
            }

            if (debito.idUsuario_fk != 0)
                return BadRequest("Não é necessário informar a id do usuário!");

            //Verifico métodos passado
            if (string.IsNullOrEmpty(debito.tipoDocumentoDebito) || debito.dataLancamentoDebito.ToString() == "01/01/0001 00:00:00" || debito.dataVencimentoDebito.ToString() == "01/01/0001 00:00:00" || debito.valorDebito == 0 || string.IsNullOrEmpty(debito.categoriaDebito))
                return BadRequest("Verifique os métodos passado");


            //Atribuo o código do usário
            var user = _context.Database.SqlQuery<string>($"select idUsuario_fk from debito where idDebito = {id}").FirstOrDefault();
            debito.idUsuario_fk = Convert.ToInt32(user);
            debito.idDebito = id;

            _context.Entry(debito).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>Método DELETE - Deleta conta/débito usuário</summary>   
        /// <returns>teste retorno</returns>
        ///<remarks> Utilize este método quando desejar deletar uma conta/débito do usuário!</remarks> 
        /// <response code="400">Algum parâmetro passado está incorreto!</response>
        /// <response code="404">Registro informado não encontrado!</response>
        [Route("deletarDebitos")]
        [HttpDelete]
        public IHttpActionResult DeleteDebitos(int id)
        {
            if (_context.Debitos.Count(x => x.idDebito == id) == 0)
            {
                return NotFound();
            }

            var debito = new Debito
            {
                idDebito = id
            };

            _context.Entry(debito).State = System.Data.Entity.EntityState.Deleted;
            _context.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

    }
}
