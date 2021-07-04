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
    [Table("usuario")]
    public class Usuario
    {
        [Key]//Responsável por dizer que é a chave primária no banco de dados
        public int idUsuario { get; set; }

        [Description("Nome do usuário")]
        [Required]
        [StringLength(45)]
        public string nomeUsuario { get; set; }

        [Description("CPF do usuário")]
        [Required]
        [StringLength(45)]
        public string cpfUsuario { get; set; }

        [Description("Senha do usuário")]
        [Required]
        [StringLength(45)]
        public string senhaUsuario { get; set; }

        [Description("E-mail do usuário")]
        [StringLength(45)]
        public string emailUsuario { get; set; }
        
        [Description("Hábilita/Desabilita usuário")]
        public bool ativoUsuario { get; set; }

        [NotMapped]
        [Description("Data do usuário")]
        public DateTime? dataCadastroUsuario { get; set; }

    }
}