using Database.Interface;
using Domain.Core;
using Matrix.QC.Database.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Database.Repository.Common
{
    public abstract class BaseRepositoryEF<TEntity> : BaseRepository<TEntity>
        where TEntity : Entity<TEntity>, new()
    {
        /// <summary>
        /// Objeto de contexto para alterações ou buscas
        /// </summary>
        protected DbContext DbContextEF { get; private set; }

        /// <summary>
        /// Objeto para aplicação de alterações e buscas
        /// </summary>
        protected DbSet<TEntity> DbSet => GetDbSet();
        private DbSet<TEntity>? _DbSet = null;

        public BaseRepositoryEF(IEntityContext contexto) : base(contexto)
        {
            // Como esta classe é específica do EF, é esperado que o context seja um DbContext nativo do EF
            this.DbContextEF = (DbContext)contexto;
        }

        public override async Task AddAsync(TEntity entity, CancellationToken cancellationToken)
        {
            await this.DbSet.AddAsync(entity, cancellationToken);
        }

        public override async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken)
        {
            await this.DbSet.AddRangeAsync(entities, cancellationToken);
        }


        public override async Task<TEntity?> GetByIdAsync(long id, CancellationToken cancellationToken)
        {
            return await this.DbSet.FindAsync(new object[] { id! }, cancellationToken);
        }


        public override async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await this.DbSet.ToListAsync(cancellationToken);
        }

        public override void Update(TEntity entity)
        {
            this.DbSet.Update(entity);
        }

        public override void UpdateRange(IEnumerable<TEntity> entities)
        {
            this.DbSet.UpdateRange(entities);
        }

        public override void Remove(TEntity entity)
        {
            this.DbSet.Remove(entity);
        }


        public override void RemoveRange(IEnumerable<TEntity> entities)
        {
            this.DbSet.RemoveRange(entities);
        }

        public override void Dispose()
        {
        }

        /// <summary>
        /// Prepara o DbSet para alterações e buscas através do contexto
        /// </summary>
        private DbSet<TEntity> GetDbSet()
        {
            if (this._DbSet == null)
            {
                // Prepara o uso da conexão
                this.Context.UseConnection();

                // Prepara o objeto para acesso ao context
                this._DbSet = this.DbContextEF.Set<TEntity>();
            }

            return this._DbSet;
        }
    }
}
