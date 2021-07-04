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
    [Table("debito")]
    public class Debito
    {
        [Key]
        public int idDebito { get; set; }
        [Description("Id do usuário")]
        public int idUsuario_fk { get; set; }
        [Description("Tipo do documento, BOLETO, CARTÃO, CREDIÁRIO")]
        [Required]
        [StringLength(45)]
        public string tipoDocumentoDebito { get; set; }
        [Description("Data que foi lançado o conta/débito")]
        [Required]
        public DateTime dataLancamentoDebito { get; set; }
        [Description("Data que irá vencer a conta/débito")]
        [Required]
        public DateTime dataVencimentoDebito { get; set; }
        [Description("Data que foi pago conta/débito")]
        public DateTime ? dataPagamentoDebito { get; set; }
        [Description("Valor da conta/débito")]
        [Required]
        public float valorDebito { get; set; }
        [Description("Beneficiário da conta/débito")]
        public int ? benificiarioDebito { get; set; }
        [Description("Obs. da conta/débito")]
        public string obsDebito { get; set; }
        [Required]
        [Description("Categoria da despesa - COMIDA, MANUTENÇÃO, COMBUSTÍVEL, SAÚDE")]
        public string categoriaDebito { get; set; }
        [Description("Valor que foi pago da conta/débito")]
        public float ? valorPagoDebito { get; set; }

    }
}