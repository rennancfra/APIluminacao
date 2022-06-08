using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class Denuncia_Rua : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Rua",
                table: "Denuncia",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rua",
                table: "Denuncia");
        }
    }
}
