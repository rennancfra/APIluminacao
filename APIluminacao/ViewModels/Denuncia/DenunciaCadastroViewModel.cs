using APIluminacao.ViewModels.Common;
using System;

namespace APIluminacao.ViewModels.Denuncia
{
    public class DenunciaCadastroViewModel : EntityViewModel
    {
        public string Descricao { get; set; } = null!;
        //public Int64 UsuarioId { get; set; }
        public string? Cep { get; set; }
        public int Numero { get; set; }
    }
}
