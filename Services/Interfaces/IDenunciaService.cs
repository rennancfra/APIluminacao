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
        public Task AddAsync(Denuncia denuncia, CancellationToken cancellationToken);
    }
}
