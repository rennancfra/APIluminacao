using Domain.Core;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Repository.Interfaces.Common
{
    public interface IRepository : IDisposable
    {
    }

    public interface IRepository<TEntity> : IRepository
        where TEntity : class, IEntity
    {
        /// <summary>
        /// Inclusão da entidade na base de dados
        /// </summary>
        Task AddAsync(TEntity entity, CancellationToken cancellationToken);

        /// <summary>
        /// Inclusão de entidades em lote na base de dados
        /// </summary>
        Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken);

        /// <summary>
        /// Carrega a entidade da base de dados a partir de um identificador
        /// </summary>
        Task<TEntity?> GetByIdAsync(long id, CancellationToken cancellationToken);

        /// <summary>
        /// Carrega todos os itens da entidade
        /// </summary>
        Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Atualiza a entidade na base de dados
        /// </summary>
        void Update(TEntity entity);

        /// <summary>
        /// Atualiza entidades em lote na base de dados
        /// </summary>
        void UpdateRange(IEnumerable<TEntity> entities);

        /// <summary>
        /// Remove a entidade da base de dados
        /// </summary>
        void Remove(TEntity entity);

        /// <summary>
        /// Remove entidades em lote da base de dados
        /// </summary>
        void RemoveRange(IEnumerable<TEntity> entities);
    }
}
