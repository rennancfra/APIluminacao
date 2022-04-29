using Database.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using System;
using System.Data.Common;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Database.Context
{
    public class DbContextBase : DbContext, IEntityContext
    {
        /// <summary>
        /// Conexão com o banco de dados
        /// </summary>
        protected DbConnection? Conexao { get; set; }

        /// <summary>
        /// Controle para verificar conexão aberta
        /// </summary>
        private bool _IsConnectionOpen { get; set; } = false;

        /// <summary>
        /// Configuração utilizada em tempo de Design para criação de migrations
        /// </summary>
        public static IConfiguration? ConfigurationMigrationArguments { get; set; }

        /// <summary>
        /// Construtor vazio dedicado à construção de migrations
        /// </summary>
        public DbContextBase()
        {
        }

        /// <summary>
        /// Construtor dedicado a injeção de dependência
        /// </summary>
        public DbContextBase(DbConnection? connection)
        {
            this.Conexao = connection;
        }

        public async Task ApplyChangesAsync(CancellationToken cancellationToken)
        {
            // Aciona o salvamento dos dados sem o accept changes para manter os registros realizados
            await this.SaveChangesAsync(false, cancellationToken);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Se não tiver conexão, o acionamento foi feito para a criação de migrations em tempo de desenvolvimento(Add-Migration)
            if (this.Conexao == null)
            {
                optionsBuilder.UseNpgsql(opt => opt.MigrationsAssembly("Database"));

                return;
            }

            optionsBuilder.UseNpgsql(this.Conexao);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Cria e aplica dinamicamente todos os configurations dos models previstos no modelTypes
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbConnection UseConnection()
        {
            // Não há como utilizar uma conexão que não foi previamente definida
            if (this.Conexao == null)
            {
                throw new Exception("A conexão com o banco de dados foi solicitada porém não foi previamente definida.");
            }

            // Tenta abrir a conexão apenas se ela ainda não estiver aberta. Isto é, se ainda estiver no primeiro uso
            if (!this._IsConnectionOpen)
            {
                try
                {
                    this.Conexao.Open();
                    this._IsConnectionOpen = true;
                }
                catch (Exception)
                {
                    throw new Exception("Falha na conexão com o banco de dados.");
                }

                // Valida se a conexão foi realmente aberta
                if (!this._IsConnectionOpen)
                {
                    throw new Exception("Falha na conexão com o banco de dados.");
                }
            }

            // Retorna a conexão em aberto
            return this.Conexao;
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken)
        {
            return await this.Database.BeginTransactionAsync(cancellationToken);
        }

        public async Task MigrateAsync(CancellationToken cancellationToken)
        {
            try
            {
                var migrator = this.Database.GetService<IMigrator>();

                await migrator.MigrateAsync(cancellationToken: cancellationToken);
            }
            catch (Exception)
            {
                throw new Exception("Não foi possível aplicar as Migrations do sistema.");
            }
        }
    }
}
