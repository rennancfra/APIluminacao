using APIluminacao.ViewModels.Usuario;
using AutoMapper;
using Domain.Models;

namespace Matrix.QC.Web.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            #region Usuario: Domain -> ViewModel
            CreateMap<Usuario, UsuarioCadastroViewModel>();
            #endregion
        }
    }
}
