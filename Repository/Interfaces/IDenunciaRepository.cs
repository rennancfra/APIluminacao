using Domain.Models;
using Repository.Interfaces.Common;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IDenunciaRepository : IRepository<Denuncia>
    {
        /// <summary>
        /// Busca um usuário de acordo com seu código no sistema
        /// </summary>
        //Task<Usuario> GetUserByCodigo(string id, CancellationToken cancellationToken);
        //Task<Denuncia> AddDenuncia(Denuncia denuncia, CancellationToken cancellationToken);
        Task<Denuncia> GetDenunciaId(long codigo, CancellationToken cancellationToken);
    }
}
