using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoBeta1.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ListaDeCompras",
                columns: table => new
                {
                    Código = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Produto = table.Column<string>(type: "varchar(200)", nullable: false),
                    Valor = table.Column<string>(type: "varchar(14)", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListaDeCompras", x => x.Código);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ListaDeCompras");
        }
    }
}
