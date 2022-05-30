using Domain.Models;
using Repository.Interfaces.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        /// <summary>
        /// Busca um usuário de acordo com seu código no sistema
        /// </summary>
        Task<Usuario> GetUsuarioByCodigoAsync(string codigo, CancellationToken cancellationToken);

        /// <summary>
        /// Verifica se um Usuário já existe na base de dados de acordo com o Código
        /// </summary>
        Task<bool> ExistsUsuarioByCodigo(string codigo, CancellationToken cancellationToken);
    }
}
