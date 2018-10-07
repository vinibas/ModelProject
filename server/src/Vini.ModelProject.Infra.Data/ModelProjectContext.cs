using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Linq;
using Vini.ModelProject.Domain;

namespace Vini.ModelProject.Infra.Data
{
    public class ModelProjectContext : DbContext
    {
        public DbSet<Usuário> Usuário { get; set; }

        public ModelProjectContext(DbContextOptions<ModelProjectContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            DesabilitarCascadeOnDeleteDeTodasAsEntidades(modelBuilder);

            var mbUsuário = modelBuilder.Entity<Usuário>();
            mbUsuário.Property(p => p.Nome)
                .HasColumnType("VARCHAR(30)");

            base.OnModelCreating(modelBuilder);
        }

        private static void DesabilitarCascadeOnDeleteDeTodasAsEntidades(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(p => p.GetForeignKeys()))
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));

            base.OnConfiguring(optionsBuilder);
        }
    }
}
