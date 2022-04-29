using Microsoft.EntityFrameworkCore.Storage;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Database.Interface
{
    /// <summary>
    /// Interface que representa um contexto de entidades com acesso à base de dados
    /// </summary>
    public interface IEntityContext
    {
        /// <summary>
        /// Abre e usa conexão com a base de dados neste contexto de entidades
        /// </summary>
        DbConnection UseConnection();

        /// <summary>
        /// Aplica as mudanças registradas nas entidades para o banco de dados
        /// </summary>
        Task ApplyChangesAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Retorna o começo de uma transação no banco de dados
        /// </summary>
        Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Aplica as migrations do sistema no banco de dados
        /// </summary>
        Task MigrateAsync(CancellationToken cancellationToken);

    }
}
