using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Swashbuckle.Examples;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Web.Http;

namespace WebApiCash.Controllers
{    
    [RoutePrefix("api/despesa")]
    public class DespesaController : ApiController
    {
        private ApplicationDbContext _context = new ApplicationDbContext();

        /// <summary>Método GET - Lista todas as despesas do usuário</summary>   
        /// <param name="value">Objeto código do usuário</param>
        /// <returns>teste retorno</returns>
        ///<remarks> Utilize este método quando desejar listar as despesas do usuário, deverá passara id do usuário! </remarks> 
        /// <response code="400">Algum parâmetro passado está incorreto!</response>
        /// <response code="200">Lista retornada com sucesso!</response>
        /// <response code="404">Nenhum valor encontrado!</response>
        [Route("buscaDespesas")]
        [HttpGet]
        public IHttpActionResult GetDespesas(string value)
        {            
            var despesas = _context.Despesas.SqlQuery($"select *from despesa where idUsuario_fk = {value}").ToList();            

            if (despesas == null)
               return NotFound();

            return Ok(despesas);
        }

        /// <summary>Método POST - Insere despesa do usuário</summary>   
        /// <returns>teste retorno</returns>
        ///<remarks> Utilize este método quando desejar inserir uma despesa do usuário, deverá passar todos os campos! </remarks> 
        /// <response code="400">Algum parâmetro passado está incorreto!</response>
        [SwaggerResponse(HttpStatusCode.Created, Type = typeof(Despesa), Description = "Despesa inserida com sucesso!")]
        [SwaggerResponseExample(HttpStatusCode.Created, typeof(DespesaResponseExamplePost), jsonConverter: typeof(StringEnumConverter))]
        [Route("inserirDespesas")]
        [HttpPost]
        public IHttpActionResult PostDespesas(Despesa despesa)
        {

            if (despesa.idUsuario_fk == 0 || string.IsNullOrEmpty(despesa.idUsuario_fk.ToString()))
                return BadRequest("É necessário informar a id do usuário");

            //Verifico métodos passado
            if (string.IsNullOrEmpty(despesa.tipoDocumentoDespesa) || despesa.dataLancamentoDespesa.ToString() == "01/01/0001 00:00:00" || despesa.dataPagamentoDespesa.ToString() == "01/01/0001 00:00:00" || despesa.valorDespesa == 0 || despesa.benificiarioDespesa == 0 || string.IsNullOrEmpty(despesa.categoriaDespesa))
                return BadRequest("Verifique os métodos passado!");


            _context.Despesas.Add(despesa);
            _context.SaveChanges();

            return CreatedAtRoute("", new { id = despesa.idDespesa }, despesa.idDespesa);
        }

        /// <summary>Método PUT - Atualiza despesa usuário</summary>        
        ///<remarks> Utilize este método quando desejar atualizar uma despesa do usuário, deverá passar todos os campos, independente de qual informação deseje atualizar! </remarks> 
        /// <response code="400">Algum parâmetro passado está incorreto!</response> 
        /// <response code="404">Nenhum valor encontrado!</response>   
        [SwaggerResponse(HttpStatusCode.NoContent, Type = typeof(Despesa), Description = "Despesa atualizada com sucesso!")]
        [SwaggerResponseExample(HttpStatusCode.NoContent, typeof(DespesaResponseExamplePut), jsonConverter: typeof(StringEnumConverter))]
        [Route("atualizaDespesas")]
        [HttpPut]
        public IHttpActionResult PutDespesas(int id, Despesa despesa)
        {
            if (!ModelState.IsValid)
                return BadRequest("Não é um modelo válido, verifique os métodos passado");

            if (_context.Despesas.Count(x => x.idDespesa == id) == 0)
            {
                return NotFound();
            }

            if (despesa.idUsuario_fk != 0)
                return BadRequest("Não é necessário informar a id do usuário!");

            //Verifico métodos passado
            if (string.IsNullOrEmpty(despesa.tipoDocumentoDespesa) || despesa.dataLancamentoDespesa.ToString() == "01/01/0001 00:00:00" || despesa.dataPagamentoDespesa.ToString() == "01/01/0001 00:00:00" || despesa.valorDespesa == 0 || despesa.benificiarioDespesa == 0 || string.IsNullOrEmpty(despesa.categoriaDespesa))
                return BadRequest("Verifique os métodos passado!");

            //Atribuo o código do usário
            var user = _context.Database.SqlQuery<string>($"select idUsuario_fk from despesa where idDespesa = {id}").FirstOrDefault();
            despesa.idUsuario_fk = Convert.ToInt32(user);
            despesa.idDespesa = id;

            _context.Entry(despesa).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
                        
            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>Método DELETE - Deleta despesa usuário</summary>   
        /// <returns>teste retorno</returns>
        ///<remarks> Utilize este método quando desejar deletar uma despesa do usuário!</remarks> 
        /// <response code="400">Algum parâmetro passado está incorreto!</response>
        /// <response code="404">Registro informado não encontrado!</response>
        [Route("deletarDespesas")]
        [HttpDelete]
        public IHttpActionResult DeleteDespesas(int id)
        {
            if (_context.Despesas.Count(x => x.idDespesa == id) == 0)
            {
                return NotFound();
            }

            var despesa = new Despesa
            {
                idDespesa = id
            };

            _context.Entry(despesa).State = System.Data.Entity.EntityState.Deleted;
            _context.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }


    }
}
