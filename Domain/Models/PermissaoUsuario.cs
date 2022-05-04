using Domain.Core;
using Domain.Enums;

namespace Domain.Models
{
    public class PermissaoUsuario : Entity<PermissaoUsuario>
    {
        public long UsuarioID { get; set; }
        public PermissaoSistemaEnum Permissao { get; set; }
    }
}
