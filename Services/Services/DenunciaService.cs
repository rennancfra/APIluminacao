using Domain.Models;
using Repository;
using Services.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Services
{
    public class DenunciaService : IDenunciaService
    {
        private readonly IUnitOfWork _uow;

        public DenunciaService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Denuncia> AddAsync(Denuncia denuncia, CancellationToken cancellationToken)
        {
            await this._uow.DenunciaRepository.AddAsync(denuncia, cancellationToken);

            await _uow.ApplyChangesAsync(cancellationToken);

            return denuncia;
        }

        public async Task<Denuncia> GetDenunciaAsync(long codigo, CancellationToken cancellationToken)
        {
            return await this._uow.DenunciaRepository.GetDenunciaId(codigo, cancellationToken);
        }
    }
}
