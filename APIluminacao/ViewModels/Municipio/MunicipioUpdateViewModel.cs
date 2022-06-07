using APIluminacao.ViewModels.Common;

namespace APIluminacao.ViewModels.Municipio
{
    public class MunicipioUpdateViewModel : EntityViewModel
    {
        public string CEP { get; set; } = null!;
    }
}
