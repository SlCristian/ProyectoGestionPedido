using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoGestionPedido.Data.Migrations
{
    /// <inheritdoc />
    public partial class Seconone : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rutas_Pedidos_IdPédido",
                table: "Rutas");

            migrationBuilder.DropIndex(
                name: "IX_Rutas_IdPédido",
                table: "Rutas");

            migrationBuilder.DropColumn(
                name: "IdPédido",
                table: "Rutas");

            migrationBuilder.CreateIndex(
                name: "IX_Rutas_IdPedido",
                table: "Rutas",
                column: "IdPedido");

            migrationBuilder.AddForeignKey(
                name: "FK_Rutas_Pedidos_IdPedido",
                table: "Rutas",
                column: "IdPedido",
                principalTable: "Pedidos",
                principalColumn: "IdPedido",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rutas_Pedidos_IdPedido",
                table: "Rutas");

            migrationBuilder.DropIndex(
                name: "IX_Rutas_IdPedido",
                table: "Rutas");

            migrationBuilder.AddColumn<int>(
                name: "IdPédido",
                table: "Rutas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Rutas_IdPédido",
                table: "Rutas",
                column: "IdPédido");

            migrationBuilder.AddForeignKey(
                name: "FK_Rutas_Pedidos_IdPédido",
                table: "Rutas",
                column: "IdPédido",
                principalTable: "Pedidos",
                principalColumn: "IdPedido",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
