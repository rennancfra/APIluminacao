using Database.Interface;
using Domain.Core;
using Repository.Interfaces.Common;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Matrix.QC.Database.Repository
{
    public abstract class BaseRepository<TEntity> : IRepository<TEntity>
        where TEntity : Entity<TEntity>, new()
    {
        /// <summary>
        /// Contexto de entidades associado a este repositório
        /// </summary>
        protected IEntityContext Context { get; }

        public BaseRepository(IEntityContext context)
        {
            this.Context = context;
        }
        public abstract Task AddAsync(TEntity entity, CancellationToken cancellationToken);

        public abstract Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken);

        public abstract Task<TEntity?> GetByIdAsync(long id, CancellationToken cancellationToken);

        public abstract Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken);

        public abstract void Update(TEntity entity);

        public abstract void UpdateRange(IEnumerable<TEntity> entities);

        public abstract void Remove(TEntity entity);

        public abstract void RemoveRange(IEnumerable<TEntity> entities);

        public abstract void Dispose();
    }
}
