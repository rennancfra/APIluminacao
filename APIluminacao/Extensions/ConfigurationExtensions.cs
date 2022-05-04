using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Services.Interfaces.Configuration;

namespace APIluminacao.Extensions
{
    /// <summary>
    /// Adiciona acesso a qualquer arquivo de configuração
    /// </summary>
    public static class ConfigurationExtensions
    {
        /// <summary>
        /// Adiciona utilitário para alterar a configuração do arquivo config
        /// </summary>
        public static IServiceCollection ConfigureWritable<T>(this IServiceCollection services, IConfigurationSection section, string file = "appsettings.json") where T : class, new()
        {
            services.Configure<T>(section);
            services.AddTransient<IWritableConfiguration<T>>(provider =>
            {
                var configuration = (IConfigurationRoot)provider.GetRequiredService<IConfiguration>();
                var environment = provider.GetRequiredService<IWebHostEnvironment>();
                var options = provider.GetRequiredService<IOptionsMonitor<T>>();
                return new WritableConfiguration<T>(environment, options, configuration, section.Key, file);
            });

            return services;
        }
    }
}
