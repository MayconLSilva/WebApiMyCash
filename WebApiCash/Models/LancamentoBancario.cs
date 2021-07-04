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
    [Table("lanbancario")]
    public class LancamentoBancario
    {
        [Key]
        public int idLanBancario { get; set; }
        [Required]
        [Description("Id do banco para lançamento bancário")]      
        public int bancoLanBancario { get; set; }
        [Required]
        [Description("Data entrada e/ou saída do valor para lançamento bancário")]
        public DateTime dataSaidaLanBancario { get; set; }
        [Required]
        [Description("Valor do lançamento bancário")]
        public float valorLanBancario { get; set; }
        [Required]
        [Description("Tipo do lançamento bancário (S) Saída ou (E) Entrada")]
        [StringLength(1)]
        public string tipoLanBancario { get; set; }
        [Description("Id do usuário")]
        public int idUsuario_fk { get; set; }

    }
}