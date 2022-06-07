using APIluminacao.ViewModels.Denuncia;
using APIluminacao.ViewModels.Municipio;
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

            #region Denuncia: Domain -> ViewModel
            CreateMap<Denuncia, DenunciaCadastroViewModel>();
            #endregion

            #region Municipio: Domain -> ViewModel
            CreateMap<MunicipioPrincipal, MunicipioCadastroViewModel>();
            #endregion
        }
    }
}
