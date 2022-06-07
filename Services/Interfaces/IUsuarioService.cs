using Domain.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IUsuarioService
    {
        /// <summary>
        /// Cria um Usuário no sistema
        /// </summary>
        Task<Usuario> CreateAsync(Usuario usuario, CancellationToken cancellationToken);

        /// <summary>
        /// Obtém todos os usuários do sistema
        /// </summary>
        Task<IEnumerable<Usuario>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Valida se o usuário pode se autenticar
        /// </summary>
        bool Authenticate(Usuario usuario, string senha);

        /// <summary>
        /// Realiza o Encrypt da senha do usuário com base no Hash informado
        /// </summary>
        string EncryptPassword(string password, string hash);
    }
}
