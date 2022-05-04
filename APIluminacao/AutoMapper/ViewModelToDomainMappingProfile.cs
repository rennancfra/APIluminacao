using APIluminacao.ViewModels.Denuncia;
using APIluminacao.ViewModels.Login;
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
        }
    }
}
