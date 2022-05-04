using Domain.Enums;
using Domain.Models;
using Repository.Interfaces.Common;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IPermissaoUsuarioRepository : IRepository<PermissaoUsuario>
    {
        /// <summary>
        /// Retorna as permissões do usuário no sistema
        /// </summary>
        Task<IEnumerable<PermissaoSistemaEnum>> GetPermissionsByUsuarioAsync(long usuarioID, CancellationToken cancellationToken);
    }
}
