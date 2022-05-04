namespace APIluminacao.ViewModels.Login
{
    public class LoginViewModel
    {
        public string Usuario { get; set; } = null!;
        public string Senha { get; set; } = null!;
        public bool ManterLogado { get; set; }
    }
}
