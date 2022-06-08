using APIluminacao.ViewModels.Denuncia;
using APIluminacao.ViewModels.Login;
using APIluminacao.ViewModels.Municipio;
using APIluminacao.ViewModels.Usuario;
using AutoMapper;
using Domain.Models;
using Domain.Transfer;

namespace Matrix.QC.Web.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            #region Commons: ViewModel -> Command
            CreateMap<DenunciaCadastroViewModel, Denuncia>();
            #endregion

            #region Login: ViewModel -> Command
            CreateMap<LoginViewModel, Login>();
            #endregion

            #region Denuncia: ViewModel -> Command
            CreateMap<DenunciaCadastroViewModel, Denuncia>();
            #endregion

            #region Usuario: ViewModel -> Command
            CreateMap<UsuarioCadastroViewModel, Usuario>();
            #endregion

            #region Municipio: ViewModel -> Command
            CreateMap<MunicipioCadastroViewModel, MunicipioPrincipal>();
            CreateMap<MunicipioUpdateViewModel, MunicipioUpdate>();
            #endregion
        }
    }
}
