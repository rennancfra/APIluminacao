using Database.Interface;
using Database.Repository.Common;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Database.Repository
{
    public class UsuarioRepositoryEF : BaseRepositoryEF<Usuario>, IUsuarioRepository
    {
        public UsuarioRepositoryEF(IEntityContext contexto) : base(contexto)
        {
        }

        public async Task<Usuario> GetUsuarioByCodigoAsync(string codigo, CancellationToken cancellationToken)
        {
            return await this.DbSet
                .Where(i => i.Codigo == codigo)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
