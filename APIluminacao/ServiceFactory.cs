using APIluminacao.AutoMapper;
using APIluminacao.Extensions;
using Database;
using Database.Context;
using Database.Interface;
using Database.Repository;
using Domain.AppSettings;
using Domain.Interface;
using Domain.Interface.Implementation;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Npgsql;
using Repository;
using Repository.Interfaces;
using Services.Interfaces;
using Services.Services;
using System;
using System.Data.Common;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using ViaCep;

namespace APIluminacao
{
    public class ServiceFactory
    {
        /// <summary>
        /// Provider temporário para acionar os migrations no início da aplicação
        /// </summary>
        private static IServiceProvider? _provider = null;

        public async static Task RegisterServices(IServiceCollection services, IConfiguration Configuration)
        {
            //Adicina ViaCep
            services.AddHttpClient<IViaCepClient, ViaCepClient>(client => { client.BaseAddress = new Uri("https://viacep.com.br/"); });

            services
                .AddSingleton<IHttpContextAccessor, HttpContextAccessor>()
                .AddTransient<IPrincipal>(provider => provider.GetService<IHttpContextAccessor>()?.HttpContext?.User!)
                .AddTransient(provider => provider.GetService<IHttpContextAccessor>()?.HttpContext?.User!)
                .AddTransient<IUsuarioLogado, UsuarioLogado>();

            // Registra serviços da aplicação
            services
                .AddScoped<IDenunciaService, DenunciaService>()
                .AddScoped<IUsuarioService, UsuarioService>()
                .AddScoped<ILoginService, LoginService>()
                .AddScoped<IAuthorizationService, AuthorizationService>()
                .AddScoped<IJwtService, JwtService>()
                .AddScoped<ITokenService, TokenService>();

            // Arquivo específico para secrets do sistema
            services
                .ConfigureWritable<TokenJwtModel>(Configuration.GetSection("token"), "appsecrets.json")
                .ConfigureWritable<ApplicationModel>(Configuration.GetSection("application"), "appsecrets.json");

            RegisterAutoMapper(services);

            // Registra pattern do banco de dados
            services
                .AddDbContext<IEntityContext, DbContextBase>(ServiceLifetime.Scoped, ServiceLifetime.Scoped)
                // Infra - Data
                .AddScoped<IUnitOfWork, UnitOfWork>()
                // Registra a conexão do banco de dados na injeção de dependência
                .AddTransient(s => CreateDatabaseConnection(s, Configuration))
                .AddLogging(builder => builder
                .AddConsole()
                .AddFilter(DbLoggerCategory.Database.Command.Name, LogLevel.Information));

            // Registra os repositórios dos bancos de dados
            services
                .AddScoped<IDenunciaRepository, DenunciaRepositoryEF>()
                .AddScoped<IUsuarioRepository, UsuarioRepositoryEF>()
                .AddScoped<IPermissaoUsuarioRepository, PermissaoUsuarioRepositoryEF>();

            // Cria o provider temporário para acionar os migrations
            _provider = services.BuildServiceProvider();

            // Executa as migrations pendentes no sistema
            await MigrateSystemAsync(new CancellationToken());
        }

        /// <summary>
        /// Método para acionar todas as migrations para atualização da base de dados
        /// </summary>
        public async static Task MigrateSystemAsync(CancellationToken cancellationToken)
        {
            if (_provider == null)
            {
                return;
            }

            await _provider.GetRequiredService<IEntityContext>().MigrateAsync(cancellationToken);
        }

        /// <summary>
        /// Registra AutoMapper das entidades
        /// </summary>
        public static void RegisterAutoMapper(IServiceCollection services)
        {
            services.AddAutoMapper((provider, cfg) => AutoMapperConfig.Configure(provider, cfg), typeof(Startup));
        }

        /// <summary>
        /// Recupera o DbConnection para ser registrado no Service Provider
        /// </summary>
        private static DbConnection CreateDatabaseConnection(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("APIluminacao");

            return new NpgsqlConnection(connectionString);
        }
    }
}
