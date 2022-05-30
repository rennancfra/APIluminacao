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
                      });
                });
        }

        /// <summary>
        /// Obtém diretório de execução do aplicativo
        /// Mesmo em debug a saída será o diretório raiz da aplicação
        /// </summary>
        public static string GetDiretorioExecucao()
        {
            bool isDebug = false;

#if DEBUG
            isDebug = true;
#endif
            if (isDebug)
            {
                var pathToExe = Process.GetCurrentProcess().MainModule?.FileName;
                return Path.GetDirectoryName(pathToExe) ?? string.Empty;
            }

            return Directory.GetCurrentDirectory();
        }
    }
}
