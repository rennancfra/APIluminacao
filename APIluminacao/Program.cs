using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;
using System.IO;
using APIluminacao.Extensions;
using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace APIluminacao
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Monta objeto Host para publicação dos serviços WEB
        /// </summary>
        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            // Configura IHostBuilder
            return Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostContext, configApp) =>
                {
                    configApp.AddJsonFile(path: "appsettings.json", optional: true, reloadOnChange: true)
                             .AddJsonFile(path: "appsecrets.json", optional: true);
                })
                .UseWindowsService()
                .ConfigureWebHostDefaults(wb =>
                {
                    var webHostBuilder = wb.UseContentRoot(GetDiretorioExecucao())
                      .UseStartup<Startup>()
                      .ConfigureKestrel(option =>
                      {
                          // Configura a porta 5000 como Http
                          option.ListenLocalhost(5000);
                          // Configura a porta 5001 como Https
                          // option.ListenLocalhost(5001, opt => UseHttps(opt, "", "")); COMENTADO POR FALTA DE CERTIFICADO
                      });
                });
        }

        /// <summary>
        /// Obtém diretório de execução do aplicativo
        /// Mesmo em debug a saída será o diretório raiz da aplicação
        /// </summary>
        public static string GetDiretorioExecucao()
        {
#if DEBUG
            var pathToExe = Process.GetCurrentProcess().MainModule?.FileName;
            return Path.GetDirectoryName(pathToExe) ?? string.Empty;
#endif

            return Directory.GetCurrentDirectory();
        }

        private static void UseHttps(ListenOptions options, string? certificatePath, string? certificatePassword)
        {
            if (string.IsNullOrEmpty(certificatePath))
            {
                options.UseHttps();
                return;
            }

            if (string.IsNullOrEmpty(certificatePassword))
            {
                options.UseHttps(certificatePath);
                return;
            }

            options.UseHttps(certificatePath, certificatePassword);
        }
    }
}
