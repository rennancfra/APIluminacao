using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Services.Interfaces;
using Domain.Enums;

namespace Services.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly ClaimsPrincipal _claimPrincipal;

        public AuthorizationService(
            ClaimsPrincipal claimPrincipal)
        {
            _claimPrincipal = claimPrincipal;
        }

        public IEnumerable<Claim> GetPermissionUsuarioClaims(IEnumerable<PermissaoSistemaEnum> permissoes)
        {
            // Estância lista de retorno
            List<Claim> claims = new List<Claim>();

            foreach (PermissaoSistemaEnum permissao in permissoes)
            {
                int chavepermissao = (int)permissao;

                // Adiciona Permissão no ClaimTypes Role, caso não exista
                if (!claims.Any(
                        c => c.Type == CustomClaimTypeEnum.Role.ToString() &&
                        c.Value == chavepermissao.ToString()))
                {
                    claims.Add(new Claim(CustomClaimTypeEnum.Role.ToString(), chavepermissao.ToString()));
                    continue;
                }

                // Cria nova claim e adiciona na lista de retorno
                claims.Add(new Claim(CustomClaimTypeEnum.Role.ToString(), chavepermissao.ToString()));
            }

            return claims;
        }

        /// <summary>
        /// Verifica a existencia de permissão no role do usuario authenticado
        /// </summary>
        public bool HasRolePermission(params PermissaoSistemaEnum[] permissoes)
        {
            foreach (PermissaoSistemaEnum permissao in permissoes)
            {
                int chave = (int)permissao;

                if (_claimPrincipal.IsInRole(chave.ToString()))
                    return true;
            }

            return false;
        }
    }
}
