using Microsoft.EntityFrameworkCore;
using ProjetoBeta1.Models;

namespace ProjetoBeta1.Data
{
    public class MinimalContextDb : DbContext
    {
        public MinimalContextDb(DbContextOptions<MinimalContextDb> options) : base(options) { }

        public DbSet<ListaDeCompras> ListaDeCompras { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ListaDeCompras>()
                .HasKey(p => p.Código);

            modelBuilder.Entity<ListaDeCompras>()
                .Property(p => p.Produto)
                .IsRequired()
                .HasColumnType("varchar(200)");

            modelBuilder.Entity<ListaDeCompras>()
                .Property(p => p.Valor)
                .IsRequired()
                .HasColumnType("varchar(14)");

            modelBuilder.Entity<ListaDeCompras>()
                .ToTable("ListaDeCompras");

            base.OnModelCreating(modelBuilder);
        }
    }
}
