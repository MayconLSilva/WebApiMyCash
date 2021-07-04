using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
//using System.Text.Json;
//using System.Text.Json.Serialization;


namespace WebApiCash
{
    [Table("despesa")]
    public class Despesa
    {
        [Key]
        public int idDespesa { get; set; }
        [Description("Id do usuário")]
        public int idUsuario_fk { get; set; }
        [Description("Tipo do documento, BOLETO, CARTÃO, CREDIÁRIO")]
        [Required]
        [StringLength(45)]
        public string tipoDocumentoDespesa { get; set; }
        [Description("Data que foi lançado a despesa")]
        [Required]
        public DateTime dataLancamentoDespesa { get; set; }
        [Description("Data que foi efetuado esta despesa")]
        [Required]
        public DateTime dataPagamentoDespesa { get; set; }
        [Description("Valor da despesa")]
        [Required]
        public float valorDespesa { get; set; }
        [Description("Beneficiário da despesa")]        
        public int ? benificiarioDespesa { get; set; }
        [Description("Obs. da despesa")]
        public string obsDespesa { get; set; }
        [Required]
        [Description("Categoria da despesa - COMIDA, MANUTENÇÃO, COMBUSTÍVEL, SAÚDE")]
        public string categoriaDespesa { get; set; }

    }
}