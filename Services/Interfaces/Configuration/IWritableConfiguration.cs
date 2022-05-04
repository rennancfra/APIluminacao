using Microsoft.Extensions.Options;
using System;

namespace Services.Interfaces.Configuration
{
    /// <summary>
    /// Interface que representa as configurações que podem ser sobrescritas
    /// </summary>
    public interface IWritableConfiguration<out T> : IOptions<T> where T : class, new()
    {
        /// <summary>
        /// Atualiza a configuração em questão no arquivo especificado
        /// </summary>
        void Update(Action<T> applyChanges);
    }
}
