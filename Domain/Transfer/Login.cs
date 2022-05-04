namespace Domain.Transfer
{
    public class Login
    {
        public string Usuario { get; set; } = null!;
        public string Senha { get; set; } = null!;
        public bool ManterLogado { get; set; }
    }
}
