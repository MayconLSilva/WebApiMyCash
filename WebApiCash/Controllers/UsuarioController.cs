using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Routing;
using Newtonsoft.Json.Converters;
using Swashbuckle.Examples;
using Swashbuckle.Swagger.Annotations;
using System.Text;
using MLSHelp;

namespace WebApiCash
{
    [RoutePrefix("api/usuario")]
    public class UsuarioController : ApiController
    {
        private MLSHelp.Util mlsHelp = new MLSHelp.Util();       
        private ApplicationDbContext _context = new ApplicationDbContext();
        
        /// <summary>Método GET - Login Uusário</summary>   
        /// <param name="value">Objeto CPF do usuário</param>
        /// <returns>teste retorno</returns>
        ///<remarks> Utilize este método para consultar e efetuar login do usuário! </remarks> 
        /// <response code="400">Algum parâmetro passado está incorreto!</response>
        /// <response code="200">Lista retornada com sucesso!</response>
        /// <response code="404">Nenhum valor encontrado!</response>  
        [SwaggerRequestExample(typeof(Usuario), typeof(UsuarioRequestExample), jsonConverter: typeof(StringEnumConverter))]        
        [Route("login")]
        [HttpGet]
        public IHttpActionResult loginUsuario(string value)
        {
            bool usuarioAtivo = false;
            string usuarioNome = "";

            if (string.IsNullOrEmpty(value))
                return BadRequest("Informe o CPF do usuário!");

            if (value.ToString().Length < 11 || value.ToString().Length > 11)
                return BadRequest("Informe um CPF válido!");            

            var UsuarioRetornado = _context.Database.SqlQuery<Usuario>("select *from usuario where cpfUsuario = @cpfUsuario", 
                                new MySqlParameter("@cpfUsuario", value)                            
                                );

            if (UsuarioRetornado.Count() == 0)
                return NotFound();


            //Pego usuário
            usuarioAtivo = UsuarioRetornado.First(x => x.cpfUsuario.Equals(value)).ativoUsuario;

            if (usuarioAtivo == false)
                return BadRequest("Usuário não está ativo");
            
            return Ok(UsuarioRetornado);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("listarUsuarios")]        
        [HttpGet]        
        public IHttpActionResult GetListaUsuario()
        {
            var usuario = _context.Usuarios;

            if (usuario == null)
                return NotFound();

            return Ok(usuario);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("buscaUsuario")]
        [HttpGet]
        public IHttpActionResult GetBuscaUsuarioCPF(string value)
        {
            //var usuario = _context.Usuarios.Find(id);
            if (string.IsNullOrEmpty(value))
                return BadRequest("Informe o CPF do usuário!");

            if (value.ToString().Length < 11 || value.ToString().Length > 11)
                return BadRequest("Informe um CPF válido!");

            var UsuarioRetornado = _context.Database.SqlQuery<Usuario>("select *from usuario where cpfUsuario = @cpfUsuario",
                                new MySqlParameter("@cpfUsuario", value)
                            );

            if (UsuarioRetornado.Count() == 0)
                return NotFound();

            return Ok(UsuarioRetornado);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("insereUsuario")]
        [HttpPost]        
        public IHttpActionResult PostUsuario(Usuario usuario)
        {           

            if (string.IsNullOrEmpty(usuario.nomeUsuario) || string.IsNullOrEmpty(usuario.cpfUsuario) || string.IsNullOrEmpty(usuario.senhaUsuario))
                return BadRequest("Verifique os objetos passado!");

            if (usuario.cpfUsuario.ToString().Length != 11)
                return BadRequest("Informe um CPF no padrão válido!");

            if (_context.Usuarios.Count(x => x.cpfUsuario == usuario.cpfUsuario) != 0)
                return BadRequest("Usuário já existente!");

            //encode
            //var teste = mlsHelp.Criptografa(usuario.nomeUsuario.ToString());
            //System.Diagnostics.Debug.WriteLine("encode " + teste);


            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
                
            return CreatedAtRoute("", new { id = usuario.idUsuario }, usuario.idUsuario);
        }

        /// <summary>Método PUT - Atualiza usuário</summary>        
        ///<remarks> Utilize este método quando desejar atualizar o usuário, deverá passar todos os campos, independente de qual informação deseje atualizar! </remarks> 
        /// <response code="400">Algum parâmetro passado está incorreto!</response> 
        /// <response code="404">Nenhum valor encontrado!</response>   
        [SwaggerResponse(HttpStatusCode.NoContent, Type = typeof(Usuario), Description = "Usuário atualizado com sucesso!")]
        [SwaggerResponseExample(HttpStatusCode.NoContent, typeof(UsuarioResponseExamplePut), jsonConverter: typeof(StringEnumConverter))]
        [Route("atualizaUsuario")]
        [HttpPut]        
        public IHttpActionResult PutUsuario(int id, Usuario usuario)
        {           
            if (!ModelState.IsValid)
                return BadRequest("Não é um modelo válido, verifique os métodos passado");

            if (_context.Usuarios.Count(x => x.idUsuario == id) == 0)
            {
                return NotFound();
            }

            //Atribuo o valor da id
            if(usuario.idUsuario == 0)
                usuario.idUsuario = id;

            if (string.IsNullOrEmpty(usuario.nomeUsuario) || string.IsNullOrEmpty(usuario.senhaUsuario) || string.IsNullOrEmpty(usuario.cpfUsuario))
                return BadRequest("Um dos objetos passado não é válido");
            
            _context.Entry(usuario).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }
               
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("deleteUsuario")]
        [HttpDelete]
        public IHttpActionResult DeleteUsuario(int value)
        {
            if (_context.Usuarios.Count(x => x.idUsuario == value) == 0)
                return NotFound();

            var usuario = _context.Usuarios.Find(value);            

            _context.Usuarios.Remove(usuario);
            _context.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>Método PATCH - Atualiza senha usuário</summary>        
        ///<remarks> Utilize este método quando desejar atualizar somente a senha do usuário, deverá passar todos os campos, independente de qual informação deseje atualizar! </remarks> 
        /// <response code="400">Algum parâmetro passado está incorreto!</response> 
        /// <response code="404">Nenhum valor encontrado!</response>   
        [SwaggerResponse(HttpStatusCode.NoContent, Type = typeof(UsuarioSenhaResponseExamplePatch), Description = "Senha do usuário atualizado com sucesso!")]
        [SwaggerResponseExample(HttpStatusCode.NoContent, typeof(UsuarioSenhaResponseExamplePatch), jsonConverter: typeof(StringEnumConverter))]
        [Route("atualizaSenha")]
        [HttpPatch]
        public IHttpActionResult PatchUsuario(int id, Usuario usuario )
        {
            if (usuario.senhaUsuario == null || usuario.senhaUsuario.ToString() == "")
                return BadRequest("Método não informado ou valor inválido, verifique!!");

            if (usuario.nomeUsuario != null || usuario.cpfUsuario != null)
                return StatusCode(HttpStatusCode.MethodNotAllowed);
                        
            var user = _context.Usuarios.Find(id);
            if (user == null)
                return NotFound();

            user.senhaUsuario = usuario.senhaUsuario.ToString();
            _context.SaveChanges();

            return Ok("Senha atualizada");
        }
        

    }
}
