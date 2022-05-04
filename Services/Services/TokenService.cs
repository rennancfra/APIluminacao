using Domain.Enums;
using Domain.Models;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Services.Services
{
    public class TokenService : ITokenService
    {
        private readonly IJwtService _jwtService;
        private readonly IAuthorizationService _authorizationService;

        public TokenService(
            IJwtService jwtService,
            IAuthorizationService authorizationService)
        {
            this._jwtService = jwtService;
            this._authorizationService = authorizationService;
        }

        public string BuildTokenUser(Usuario usuario, bool manterLogado, IEnumerable<PermissaoSistemaEnum> permissoes)
        {
            // O token de usuário normal não há nenhuma claim específica
            IEnumerable<Claim> claims = BuildUserClaims(usuario, manterLogado, permissoes);

            // Cria o token de forma específica para Usuário comum para disponibilização ao client
            return this._jwtService.CreateToken(claims, manterLogado);
        }

        private IEnumerable<Claim> BuildUserClaims(Usuario usuario, bool manterLogado, IEnumerable<PermissaoSistemaEnum> permissoes)
        {
            // Insere claims a partir das permissões disponíveis
            IEnumerable<Claim> claims = this._authorizationService.GetPermissionUsuarioClaims(permissoes);

            // Insere claims dos dados do usuário
            return claims.Concat(this.BuildUserBasicClaims(usuario, manterLogado));
        }

        private IEnumerable<Claim> BuildUserBasicClaims(Usuario usuario, bool manterLogado)
        {
            yield return new Claim(CustomClaimTypeEnum.Sid.ToString(), usuario.ID.ToString()!);
            yield return new Claim(CustomClaimTypeEnum.NameIdentifier.ToString(), usuario.Codigo ?? string.Empty);
            yield return new Claim(CustomClaimTypeEnum.Name.ToString(), usuario.Nome ?? string.Empty);
            yield return new Claim(CustomClaimTypeEnum.Email.ToString(), usuario.Email ?? string.Empty);
            yield return new Claim(CustomClaimTypeEnum.MobilePhone.ToString(), usuario.Celular ?? string.Empty);
            yield return new Claim(CustomClaimTypeEnum.ManterLogado.ToString(), manterLogado ? "1" : "0");
        }
    }

}
