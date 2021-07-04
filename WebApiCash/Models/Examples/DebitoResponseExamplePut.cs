using Swashbuckle.Examples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiCash
{
    internal class DebitoResponseExamplePut : IExamplesProvider
    {
        public object GetExamples()
        {
            return new Debito 
            {
                idDebito = 1,                
                idUsuario_fk = 1,
                tipoDocumentoDebito = "BOLETO",
                dataLancamentoDebito = Convert.ToDateTime("2021-01-01 23:52:21"),
                dataVencimentoDebito = Convert.ToDateTime("2021-06-29 23:52:21"),
                dataPagamentoDebito = Convert.ToDateTime("2021-06-29 23:52:21"),
                valorDebito = 100.00F,
                benificiarioDebito = 0,
                obsDebito = "Despesa ref. dentista Marcos Zapct",
                categoriaDebito = "SAÚDE",
                valorPagoDebito = 100.00f
            };
        }
    }
}