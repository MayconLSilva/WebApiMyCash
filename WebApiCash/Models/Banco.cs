using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using System.ComponentModel;

namespace WebApiCash
{
    [Table("banco")]
    public class Banco
    {
        [Key]
        public int idBanco { get; set; }
        [Description("Nome do banco")]        
        [Required]
        [StringLength(45)]        
        public string nomeBanco { get; set; }
        [Description("Id do usuário")]
        [Required]
        public int idUsuario_fk { get; set; }        
    }
}