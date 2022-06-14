using Domain.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IDenunciaService
    {
        /// <summary>
        /// Adiona uma denúncia no banco de dados
        /// </summary>
        public Task<Denuncia> AddAsync(Denuncia denuncia, CancellationToken cancellationToken);
        public Task<Denuncia> GetDenunciaAsync(long codigo, CancellationToken cancellationToken);
        public Task<Denuncia> UpdateStatusDenuncia(long codigo, bool finalizado, CancellationToken cancellationToken);
    }
}
