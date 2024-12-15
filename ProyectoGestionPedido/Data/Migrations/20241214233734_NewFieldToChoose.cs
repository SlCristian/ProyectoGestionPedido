using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoGestionPedido.Data.Migrations
{
    /// <inheritdoc />
    public partial class NewFieldToChoose : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TipoCliente",
                table: "Clientes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TipoCliente",
                table: "AspNetUsers",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TipoCliente",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "TipoCliente",
                table: "AspNetUsers");
        }
    }
}
