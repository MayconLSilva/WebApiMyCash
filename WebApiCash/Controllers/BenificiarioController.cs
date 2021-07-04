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
    [RoutePrefix("api/beneficiario")]
    public class BenificiarioController : ApiController
    {
        private ApplicationDbContext _context = new ApplicationDbContext();

        /// <summary>Método GET - Lista todos beneficiários do usuário</summary>   
        /// <param name="value">Objeto código do usuário</param>
        /// <returns>teste retorno</returns>
        ///<remarks> Utilize este método quando desejar listar os beneficiários do usuário, deverá passara id do usuário! </remarks> 
        /// <response code="400">Algum parâmetro passado está incorreto!</response>
        /// <response code="200">Lista retornada com sucesso!</response>
        /// <response code="404">Nenhum valor encontrado!</response>
        [Route("buscaBeneficiarios")]
        [HttpGet]
        public IHttpActionResult GetBeneficiarios(string value)
        {
            var benefi = _context.Benificiarios.SqlQuery($"select *from benificiario where idUsuario_fk = {value}");

            if (benefi == null)
                return NotFound();

            return Ok(benefi);
        }

        /// <summary>Método POST - Insere beneficiário</summary>   
        /// <returns>teste retorno</returns>
        ///<remarks> Utilize este método quando desejar inserir um beneficiário do usuário, deverá passar todos os campos! </remarks> 
        /// <response code="400">Algum parâmetro passado está incorreto!</response>
        [SwaggerResponse(HttpStatusCode.Created, Type = typeof(Benificiario), Description = "Beneficiário inserido com sucesso!")]
        [SwaggerResponseExample(HttpStatusCode.Created, typeof(BeneficiarioResponseExamplePost), jsonConverter: typeof(StringEnumConverter))]
        [Route("inserirBeneficiarios")]
        [HttpPost]
        public IHttpActionResult PostBeneficiarios(Benificiario benificiario)
        {

            if (benificiario.idUsuario_fk == 0 || string.IsNullOrEmpty(benificiario.idUsuario_fk.ToString()))
                return BadRequest("É necessário informar a id do usuário");

            _context.Entry(benificiario).State = System.Data.Entity.EntityState.Added;
            _context.SaveChanges();

            return CreatedAtRoute("", new { id = benificiario.idBenificiario }, benificiario.idBenificiario);
        }

        /// <summary>Método PUT - Atualiza beneficiário</summary>        
        ///<remarks> Utilize este método quando desejar atualizar o beneficiário do usuário, deverá passar todos os campos, independente de qual informação deseje atualizar! </remarks> 
        /// <response code="400">Algum parâmetro passado está incorreto!</response> 
        /// <response code="404">Nenhum valor encontrado!</response>   
        [SwaggerResponse(HttpStatusCode.NoContent, Type = typeof(Benificiario), Description = "Beneficiário atualizado com sucesso!")]
        [SwaggerResponseExample(HttpStatusCode.NoContent, typeof(BeneficiarioResponseExamplePut), jsonConverter: typeof(StringEnumConverter))]
        [Route("atualizaBeneficiarios")]
        [HttpPut]
        public IHttpActionResult PutBeneficiarios(int id, Benificiario benificiario)
        {
            if (!ModelState.IsValid)
                return BadRequest("Não é um modelo válido, verifique os métodos passado");

            if (_context.Benificiarios.Count(x => x.idBenificiario == id) == 0)
            {
                return NotFound();
            }

            if (benificiario.idUsuario_fk != 0)
                return BadRequest("Não é necessário informar a id do usuário!");

            //Verifico métodos passado
            if (string.IsNullOrEmpty(benificiario.nomeBenificiario))
                return BadRequest("Verifique os métodos passado!");

            //Atribuo o código do usário
            var user = _context.Database.SqlQuery<string>($"select idUsuario_fk from benificiario where idBenificiario = {id}").FirstOrDefault();
            benificiario.idUsuario_fk = Convert.ToInt32(user);
            benificiario.idBenificiario = id;

            _context.Entry(benificiario).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>Método DELETE - Deleta beneficiário</summary>   
        /// <returns>teste retorno</returns>
        ///<remarks> Utilize este método quando desejar deletar um beneficiário do usuário!</remarks> 
        /// <response code="400">Algum parâmetro passado está incorreto!</response>
        /// <response code="404">Registro informado não encontrado!</response>
        [SwaggerRequestExample(typeof(Benificiario), typeof(BeneficiarioResponseExamplePut), jsonConverter: typeof(StringEnumConverter))]
        [Route("deletarBeneficiarios")]
        [HttpDelete]
        public IHttpActionResult DeleteBeneficiarios(int id)
        {
            if (_context.Benificiarios.Count(x => x.idBenificiario == id) == 0)
            {
                return NotFound();
            }

            var benificiario = new Benificiario
            {
                idBenificiario = id
            };

            _context.Entry(benificiario).State = System.Data.Entity.EntityState.Deleted;
            _context.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

    }
}
