using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Services.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Aplica a criptografia SHA256 utilizando uma string
        /// </summary>
        public static string CryptographySHA256(this string valor)
        {
            StringBuilder builder = new StringBuilder();

            // Instancia o objeto para criptografar o valor recebido
            using (SHA256 objSHA = SHA256Managed.Create())
            {
                // Utiliza o encoding UTF8 para obter byte a byte do valor recebido
                Encoding objEncoding = Encoding.UTF8;
                byte[] hash = objSHA.ComputeHash(objEncoding.GetBytes(valor));

                // Converte cada byte obtido para hexadecimal (ToString("x2") = 2 caracteres Hexadecimais uppercase para cada byte)
                foreach (byte b in hash)
                {
                    builder.Append(b.ToString("x2"));
                }
            }

            return builder.ToString();
        }
    }
}
