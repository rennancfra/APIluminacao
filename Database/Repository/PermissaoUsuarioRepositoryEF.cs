using Database.Interface;
using Database.Repository.Common;
using Domain.Enums;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Database.Repository
{
    public class PermissaoUsuarioRepositoryEF : BaseRepositoryEF<PermissaoUsuario>, IPermissaoUsuarioRepository
    {
        public PermissaoUsuarioRepositoryEF(IEntityContext contexto) : base(contexto)
        {
        }

        public async Task<IEnumerable<PermissaoSistemaEnum>> GetPermissionsByUsuarioAsync(long usuarioID, CancellationToken cancellationToken)
        {
            return await this.DbSet
                .Where(pu => pu.UsuarioID == usuarioID)
                .Select(pu => pu.Permissao)
                .ToListAsync(cancellationToken);
        }
    }
}
