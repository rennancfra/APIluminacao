using APIluminacao.ViewModels.Common;

namespace APIluminacao.ViewModels.Municipio
{
    public class MunicipioCadastroViewModel : EntityViewModel
    {
        public string Nome { get; set; } = null!;
        public string UF { get; set; } = null!;
    }
}
