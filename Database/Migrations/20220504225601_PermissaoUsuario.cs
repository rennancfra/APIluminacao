using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using System.Text;

namespace Database.Migrations
{
    public partial class PermissaoUsuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Codigo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Nome = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Senha = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Hash = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Email = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: true),
                    Celular = table.Column<string>(type: "character varying(13)", maxLength: 13, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PermissaoUsuario",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UsuarioID = table.Column<long>(type: "bigint", nullable: false),
                    Permissao = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissaoUsuario", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PermissaoUsuario_Usuario_UsuarioID",
                        column: x => x.UsuarioID,
                        principalTable: "Usuario",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PermissaoUsuario_UsuarioID",
                table: "PermissaoUsuario",
                column: "UsuarioID");

            // Cria um string builder
            StringBuilder sql = new();

            // Insere o usuário admin
            sql.Append(@"INSERT INTO public.""Usuario"" (""Codigo"", ""Nome"", ""Senha"", ""Email"", ""Celular"", ""Hash"") VALUES ('admin', 'Usuário Master', 'ddb66efe757d9941cd318ec10c188d57370f20217c70707365518b4fcf61ddd3', null, null, '1b1bd2bb-8711-4aae-b6c9-36fd0b35cadd')");

            // Executa o script montado
            migrationBuilder.Sql(sql.ToString(), true);

            // Zera a referência do builder
            sql = new();

            // Insere a permissão de usuário master
            sql.Append(@"INSERT INTO public.""PermissaoUsuario"" (""UsuarioID"", ""Permissao"") VALUES (1, 4)");

            // Executa o script montado
            migrationBuilder.Sql(sql.ToString(), true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PermissaoUsuario");

            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
