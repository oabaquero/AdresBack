using Microsoft.EntityFrameworkCore;

namespace Adres.Models
{
    public class AdresContext : DbContext 
    {
        public AdresContext(DbContextOptions<AdresContext> options):base (options)
        {
        }   

        public DbSet<Adquisicion> Adquisiciones {get; set;}
        public DbSet<Parametrica> Parametricas {get; set;}
         public DbSet<Historico> Historicos {get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Adquisicion>()
                .HasOne(a => a.Unidades)
                .WithMany()
                .HasForeignKey(a => a.UnidadId);

            modelBuilder.Entity<Adquisicion>()
                .HasOne(a => a.Bienes)
                .WithMany()
                .HasForeignKey(a => a.BienId);

            modelBuilder.Entity<Adquisicion>()
                .HasOne(a => a.Proveedores)
                .WithMany()
                .HasForeignKey(a => a.ProveedorId);
        }
    }
    
}