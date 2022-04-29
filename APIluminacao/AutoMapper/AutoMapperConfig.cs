using AutoMapper;
using Matrix.QC.Web.AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace APIluminacao.AutoMapper
{
    public static class AutoMapperConfig
    {
        public static void Configure(IServiceProvider provider, IMapperConfigurationExpression cfg)
        {
            cfg.AddProfile<DomainToViewModelMappingProfile>();
            cfg.AddProfile<ViewModelToDomainMappingProfile>();
            cfg.ConstructServicesUsing(type => ActivatorUtilities.CreateInstance(provider, type));
        }
    }
}
