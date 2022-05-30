using Microsoft.EntityFrameworkCore.Migrations;
using System.Text;

namespace Database.Migrations
{
    public partial class Usuario_Ativo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "Usuario",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            // Cria um string builder
            StringBuilder sql = new();

            // Insere o usuário admin
            sql.Append(@"INSERT INTO public.""Usuario"" (""Codigo"", ""Nome"", ""Senha"", ""Email"", ""Celular"", ""Hash"", ""Ativo"") VALUES ('admin', 'Usuário Master', 'ddb66efe757d9941cd318ec10c188d57370f20217c70707365518b4fcf61ddd3', null, null, '1b1bd2bb-8711-4aae-b6c9-36fd0b35cadd', True)");

            // Executa o script montado
            migrationBuilder.Sql(sql.ToString(), true);

            // Zera a referência do builder
            sql = new();

            // Insere a permissão de usuário master
            sql.Append(@"INSERT INTO public.""PermissaoUsuario"" (""UsuarioID"", ""Permissao"") VALUES (1, 3)");

            // Executa o script montado
            migrationBuilder.Sql(sql.ToString(), true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "Usuario");
        }
    }
}
