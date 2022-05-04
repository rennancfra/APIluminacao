using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Threading;
using Domain.Enums;

namespace Services.Interfaces
{
    /// <summary>
    /// Interface que disponibiliza serviços de permissões e autorização para o sistema
    /// </summary>
    public interface IAuthorizationService
    {
        /// <summary>
        /// Retorna Claims com permissões configuradas para o usuario, possibilitando a utilização no token
        /// </summary>
        IEnumerable<Claim> GetPermissionUsuarioClaims(IEnumerable<PermissaoSistemaEnum> permissoes);

        /// <summary>
        /// Verifica se alguma das permissões existe na claim Role do usuário logado
        /// </summary>
        bool HasRolePermission(params PermissaoSistemaEnum[] permissoes);
    }
}
