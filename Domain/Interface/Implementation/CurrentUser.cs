using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interface.Implementation
{
    /// <summary>
    /// Classe que representa o acesso ao usuário logado no sistema
    /// </summary>
    public class UsuarioLogado : IUsuarioLogado
    {
        /// <summary>
        /// Principal e Identity do usuário logado no sistema
        /// </summary>
        private readonly ClaimsPrincipal _claimsPrincipal;

        public UsuarioLogado(ClaimsPrincipal claimsPrincipal)
        {
            this._claimsPrincipal = claimsPrincipal;
        }

        public long? ID => GetClaimValue<long?>(CustomClaimTypeEnum.Sid.ToString());
        public string? Codigo => GetClaimValue<string?>(CustomClaimTypeEnum.NameIdentifier.ToString());
        public string? Nome => GetClaimValue<string?>(CustomClaimTypeEnum.Name.ToString());
        public string? Email => GetClaimValue<string?>(CustomClaimTypeEnum.Email.ToString());
        public string? Celular => GetClaimValue<string?>(CustomClaimTypeEnum.MobilePhone.ToString());
        public bool ManterLogado => GetClaimValue<string>(CustomClaimTypeEnum.ManterLogado.ToString()) == "1";

        /// <summary>
        /// Procura o claim solicitado e retorna o valor existente
        /// </summary>
        private T GetClaimValue<T>(string claimType)
        {
            // Procura o claim solicitado
            Claim? claim = this._claimsPrincipal?.FindFirst(claimType);

            // Sai com default se não encontrou o claim
            if (claim == null || string.IsNullOrWhiteSpace(claim.Value))
            {
                return default!;
            }

            // Sai se o valor já for do tipo esperado
            if (claim.Value is T value)
            {
                return value;
            }

            try
            {
                // Trata nullable types
                if (Nullable.GetUnderlyingType(typeof(T)) != null)
                {
                    return (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFrom(claim.Value);
                }

                // Converte para o type solicitado
                return (T)Convert.ChangeType(claim.Value, typeof(T));
            }
            catch
            {
                // Se deu algum erro de casting, retorna o default do type solicitado
                return default!;
            }
        }
    }
}
