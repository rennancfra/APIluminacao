using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Repository;
using Repository.Interfaces;
using Database.Interface;
using System.Data.Common;
using Microsoft.EntityFrameworkCore.Storage;

namespace Database
{
    public class UnitOfWork : IUnitOfWork
    {

        #region Propriedades

        /// <summary>
        /// Contexto padrão de entidades centralizado para todos os repositórios deste objeto
        /// </summary>
        public IEntityContext DefaultContext { get; }

        /// <summary>
        /// Provider de serviços do sistema para processar as alterações no banco de dados
        /// </summary>
        protected IServiceProvider _serviceProvider { get; }

        #region Repositórios Principais
        public IDenunciaRepository DenunciaRepository => this._serviceProvider.GetRequiredService<IDenunciaRepository>();
        public IUsuarioRepository UsuarioRepository => this._serviceProvider.GetRequiredService<IUsuarioRepository>();
        #endregion

        #endregion

        #region Construtores
        public UnitOfWork(IEntityContext DefaultContext, IServiceProvider serviceProvider)
        {
            this.DefaultContext = DefaultContext;
            this._serviceProvider = serviceProvider;
        }
        #endregion

        #region Métodos Public

        /// <summary>
        /// Testa a conexão do contexto padrão
        /// </summary>
        public void ConnectionTestDefault()
        {
            this.DefaultContext.UseConnection();
        }

        /// <summary>
        /// Aplica as mudanças realizadas nos objetos para o banco de dados
        /// </summary>
        public async Task ApplyChangesAsync(CancellationToken cancellationToken)
        {
            // Inicia a transação com o banco de dados
            await using (IDbContextTransaction transaction = await this.DefaultContext.BeginTransactionAsync(cancellationToken))
            {
                try
                {
                    await this.DefaultContext.ApplyChangesAsync(cancellationToken);

                    // Aplica o commit da transação
                    await transaction.CommitAsync(cancellationToken);
                }
                catch (DbUpdateConcurrencyException concurrencyExc)
                {
                    // Lança a Exception customizada do sistema para erros de concorrência
                    throw new Exception("Concorrência detectada durante o salvamento dos dados.", concurrencyExc);
                }
            }
        }

        public void Dispose()
        {
        }
        #endregion
    }
}
