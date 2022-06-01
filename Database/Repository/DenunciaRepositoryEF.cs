using Database.Interface;
using Database.Repository.Common;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Database.Repository
{
    public class DenunciaRepositoryEF : BaseRepositoryEF<Denuncia>, IDenunciaRepository
    {
        public DenunciaRepositoryEF(IEntityContext contexto) : base(contexto)
        {
        }

        public async Task<Denuncia> GetDenunciaId(long codigo, CancellationToken cancellationToken)
        {
            //return await this.GetByIdAsync(codigo, cancellationToken);
            return await this.DbSet.Where(x => x.ID == codigo).FirstOrDefaultAsync(cancellationToken);
        }

        //public async Task<Denuncia> AddDenuncia(Denuncia denuncia, CancellationToken cancellationToken)
        //{
        //    return await this.DbSet.AddAsync(denuncia, cancellationToken);

        //}

        //public async Task<Usuario> GetUserByCodigo(string id, CancellationToken cancellationToken)
        //{
        //    return await this.DbSet.Where(x => x.UsuarioId == id).FirstOrDefaultAsync(cancellationToken);
        //}
    }
}
