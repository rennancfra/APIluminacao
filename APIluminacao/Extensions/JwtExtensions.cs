using Domain.Transfer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace APIluminacao.Extensions
{
    /// <summary>
    /// Classe de extensão para configuração para o Jwt Authorization
    /// </summary>
    public static class JwtExtensions
    {
        /// <summary>
        /// Configura a autorização para o token jwt
        /// </summary>
        public static IServiceCollection AddJwtAuthorization(this IServiceCollection services)
        {
            // Carrega o serviço de token para obter os dados necessários para a configuração do JwtBearer
            IServiceProvider provider = services.BuildServiceProvider();
            IJwtService tokenService = provider.GetRequiredService<IJwtService>();

            TokenBearerParameters bearerParameters = tokenService.GetTokenBearerParameters();

            // Configura a autenticação via Bearer JWT
            services
                .AddAuthentication()
                // Adiciona configurações de comportamento de autenticação via Bearer/JWT
                .AddJwtBearer("Bearer", bearerOptions =>
                {
                    BuildValidationParameters(bearerOptions, bearerParameters);
                });

            services.AddAuthorization(auth =>
            {
                // Adiciona policy Bearer para autenticação dos tokens
                auth.AddPolicy(
                    "Bearer",
                    new AuthorizationPolicyBuilder()
                        .AddAuthenticationSchemes("Bearer")
                        .RequireAuthenticatedUser()
                        .Build()
                );
            });

            return services;
        }

        /// <summary>
        /// Monta os parâmetros de validação
        /// </summary>
        public static void BuildValidationParameters(JwtBearerOptions bearerOptions, TokenBearerParameters bearerParameters)
        {
            var paramsValidation = bearerOptions.TokenValidationParameters;
            paramsValidation.IssuerSigningKey = bearerParameters.SigningKey;
            paramsValidation.ValidAudience = bearerParameters.Audience;
            paramsValidation.ValidIssuer = bearerParameters.Issuer;

            // Valida o aud do token
            paramsValidation.ValidateAudience = true;

            // Valida a assinatura de um token recebido
            paramsValidation.ValidateIssuerSigningKey = true;

            // Verifica se um token recebido ainda é válido
            paramsValidation.ValidateLifetime = true;

            // Tempo de tolerância para a expiração de um token
            paramsValidation.ClockSkew = TimeSpan.Zero;

            // Ao utilizar claimTypes customizadas é necessário informar ao ClaimsPrincipal
            // para que o IsInRole utilize o type customizado na validação.
            paramsValidation.RoleClaimType = Domain.Enums.CustomClaimTypeEnum.Role.ToString();
        }
    }
}
