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
    }
}
