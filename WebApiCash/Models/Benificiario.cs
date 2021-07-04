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
    [Table("benificiario")]
    public class Benificiario
    {
        [Key]
        public int idBenificiario { get; set; }
        [Description("Nome do beneficiário do débito e/ou despesa")]
        [Required]
        [StringLength(45)]
        public string nomeBenificiario { get; set; }
        [Description("Id do usuário")]
        public int idUsuario_fk { get; set; }

    }
}