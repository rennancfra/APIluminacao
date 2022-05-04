using Domain.AppSettings;
using Domain.Transfer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Services.Interfaces;
using Services.Interfaces.Configuration;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Services.Services
{
    public class JwtService : IJwtService
    {
        /// <summary>
        /// Audiência do JWT
        /// </summary>
        public string Audience = "api-iluminacao-client";

        /// <summary>
        /// Issuer do JWT
        /// </summary>
        private readonly string Issuer = "api-iluminacao";

        /// <summary>
        /// Service de configuração do sistema
        /// </summary>
        private readonly IConfiguration _config;

        /// <summary>
        /// Service de atualização de configuração do sistema
        /// </summary>
        private readonly IWritableConfiguration<TokenJwtModel> _writableConfig;

        public JwtService(IConfiguration config, IWritableConfiguration<TokenJwtModel> writableConfig)
        {
            _config = config;
            _writableConfig = writableConfig;
        }

        public string CreateToken(IEnumerable<Claim> claimsIdentity, bool manterLogado = false)
        {
            // Define a data de validade do token
            DateTime dataValidade = this.GetTokenValidationTime(manterLogado);

            // Remove "aud" Audience caso já esteja na lista (renovação de token)
            // Pois na renovação um valor adicional é colocado na lista e eventualmente o payload fica gigantesco, causando:
            // BadRequest (400 Request Header Or Cookie Too Large)
            var claimsList = claimsIdentity.Where(c => c.Type != JwtRegisteredClaimNames.Aud).ToList();

            // Cria payload com dados do JWT
            var payload = new JwtPayload(Issuer, Audience, claimsList, null, dataValidade);

            // Chave de segurança da assinatura do jwt
            var SigningKey = this.GetSecurityKey();

            // Gera credenciais para o token
            var signingCredentials = new SigningCredentials(SigningKey, SecurityAlgorithms.HmacSha256Signature, SecurityAlgorithms.Sha256Digest);

            // Cria header do JWT
            var header = new JwtHeader(signingCredentials);

            // Monta token JWT
            var secToken = new JwtSecurityToken(header, payload);
            var handler = new JwtSecurityTokenHandler();

            // Gera a string do token para ser enviada para o cliente
            var tokenString = handler.WriteToken(secToken);

            return tokenString;
        }

        public TokenBearerParameters GetTokenBearerParameters()
        {
            return new TokenBearerParameters()
            {
                SigningKey = GetSecurityKey(),
                Audience = Audience,
                Issuer = Issuer
            };
        }

        /// <summary>
        /// Gera uma chave de segurança a partir da chave privada recebida
        /// </summary>
        protected SecurityKey GetSecurityKey()
        {
            // Pega a chave privada que deve ficar somente no servidor e bem protegida
            var CommunicationKey = GetPrivateKey();

            // Gera a chave de segurança a partir da chave privada obtida
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(CommunicationKey));
        }

        /// <summary>
        /// Obtém a chave privada para o token Jwt a partir do arquivo configuration default (appsettings)
        /// </summary>
        private string GetPrivateKey()
        {
            // Procura pelo token atual do arquivo de configuração
            TokenJwtModel token = _config
                    .GetSection("token")
                    .Get<TokenJwtModel>();

            // Retorna se o token já é válido
            if (token != null)
            {
                return token.Key;
            }

            // Cria uma nova chave RSA de 2048 bits
            using var cryptoProvider = new RSACryptoServiceProvider(2048);

            // Exporta os parâmetros da chave criada
            RSAParameters parametersKey = cryptoProvider.ExportParameters(true);

            // Utiliza um dos parâmetros como chave privada
            string privateKey = parametersKey.P != null ? Convert.ToBase64String(parametersKey.P) : string.Empty;

            // Atualiza o arquivo appsettings
            _writableConfig.Update((a) =>
            {
                // Define a chave para o token e pega o objeto para retorno
                a.Key = privateKey;
                token = a;
            });

            // Retorna a nova chave privada que acabou de ser inserida
            return token?.Key ?? string.Empty;
        }

        public DateTime GetTokenValidationTime(bool manterLogado = false)
        {
            return manterLogado ? DateTime.Now.AddDays(7) : DateTime.Now.AddHours(4);
        }
    }

}
