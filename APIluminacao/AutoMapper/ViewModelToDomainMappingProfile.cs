using APIluminacao.ViewModels.Denuncia;
using AutoMapper;
using Domain.Models;

namespace Matrix.QC.Web.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            #region Commons: ViewModel -> Command
            CreateMap<DenunciaCadastroViewModel, Denuncia>();
            #endregion
        }
    }
}
