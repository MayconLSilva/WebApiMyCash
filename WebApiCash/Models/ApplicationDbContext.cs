using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using MySql.Data.EntityFramework;

namespace WebApiCash
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Despesa> Despesas { get; set; }
        public DbSet<Banco> Bancos{ get; set; }
        public DbSet<Benificiario> Benificiarios { get; set; }
        public DbSet<LancamentoBancario> LancamentosBanc { get; set; }
        public DbSet<Debito> Debitos { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection")
         {


         }
    }
}