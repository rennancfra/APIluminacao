using APIluminacao.ViewModels.Common;

namespace APIluminacao.ViewModels.Usuario
{
    public class UsuarioCadastroViewModel : EntityViewModel
    {
        public string Nome { get; set; } = null!;
        public string? Senha { get; set; }
        public string? Email { get; set; }
        public string? Celular { get; set; }
        public bool Ativo { get; set; }
    }
}
