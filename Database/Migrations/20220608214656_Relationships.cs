using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class Relationships : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UsuarioId",
                table: "Denuncia",
                newName: "UsuarioID");

            migrationBuilder.RenameColumn(
                name: "Cep",
                table: "Denuncia",
                newName: "CEP");

            migrationBuilder.CreateIndex(
                name: "IX_Denuncia_UsuarioID",
                table: "Denuncia",
                column: "UsuarioID");

            migrationBuilder.AddForeignKey(
                name: "FK_Denuncia_Usuario_UsuarioID",
                table: "Denuncia",
                column: "UsuarioID",
                principalTable: "Usuario",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Denuncia_Usuario_UsuarioID",
                table: "Denuncia");

            migrationBuilder.DropIndex(
                name: "IX_Denuncia_UsuarioID",
                table: "Denuncia");

            migrationBuilder.RenameColumn(
                name: "UsuarioID",
                table: "Denuncia",
                newName: "UsuarioId");

            migrationBuilder.RenameColumn(
                name: "CEP",
                table: "Denuncia",
                newName: "Cep");
        }
    }
}
