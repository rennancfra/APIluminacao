using Domain.Transfer;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Services.Interfaces
{
    public interface IJwtService
    {
        /// <summary>
        /// Gera um novo token para autenticação com o sistema a partir de claims já conhecidos
        /// </summary>
        string CreateToken(IEnumerable<Claim> claimsIdentity, bool manterLogado = false);

        /// <summary>
        /// Obtém os parâmetros para configuração da autenticação via Bearer Token
        /// </summary>
        TokenBearerParameters GetTokenBearerParameters();

        /// <summary>
        /// Obtém a data de validade do token
        /// </summary>
        DateTime GetTokenValidationTime(bool manterLogado = false);
    }
}
