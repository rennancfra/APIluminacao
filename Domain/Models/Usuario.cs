using Domain.Core;
using System.Collections.Generic;

namespace Domain.Models
{
    public class Usuario : Entity<Usuario>
    {
        public string Codigo { get; set; } = null!;
        public string Nome { get; set; } = null!;
        public string? Senha { get; set; }
        public string? Hash { get; set; }
        public string? Email { get; set; }
        public string? Celular { get; set; }
        public bool Ativo { get; set; }
        public List<PermissaoUsuario>? Permissoes { get; set; }
    }
}
