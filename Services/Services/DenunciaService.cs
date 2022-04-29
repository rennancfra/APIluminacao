using Domain.Models;
using Repository.Interfaces;
using Services.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Services
{
    public class DenunciaService : IDenunciaService
    {
        private readonly IDenunciaRepository _repository;

        public DenunciaService(IDenunciaRepository repository)
        {
            _repository = repository;
        }

        public async Task AddAsync(Denuncia denuncia, CancellationToken cancellationToken)
        {
            await this._repository.AddAsync(denuncia, cancellationToken);
        }
    }
}
