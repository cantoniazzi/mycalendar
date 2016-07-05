using Agenda.Entities;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Agenda.DAL.Context
{
    public class DBContext : DbContext
    {
        public DBContext() : base("AgendaDBContext") { }

        public DbSet<Entities.Agenda> Agendas { get; set; }

        public DbSet<Compromisso> Compromissos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }
    }
}
