namespace Domain.AppSettings
{
    /// <summary>
    /// Modelo com propriedades do token Jwt. Conversão do arquivo de configuração para objeto
    /// </summary>
    public class TokenJwtModel
    {
        /// <summary>
        /// Chave privada do token Jwt
        /// </summary>
        public string Key { get; set; } = null!;
    }
}
