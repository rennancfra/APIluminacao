using APIluminacao.ViewModels.Common;
using System;

namespace APIluminacao.ViewModels.Denuncia
{
    public class DenunciaCadastroViewModel : EntityViewModel
    {
        public string Descricao { get; set; } = null!;
        public string? CEP { get; set; }
        public string Rua { get; set; } = null!;
        public int Numero { get; set; }
    }
}
