// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProjetoBeta1.Data;

#nullable disable

namespace ProjetoBeta1.Migrations
{
    [DbContext(typeof(MinimalContextDb))]
    [Migration("20221226133035_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ProjetoBeta1.Models.ListaDeCompras", b =>
                {
                    b.Property<Guid>("Código")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<string>("Produto")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Valor")
                        .IsRequired()
                        .HasColumnType("varchar(14)");

                    b.HasKey("Código");

                    b.ToTable("ListaDeCompras", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
