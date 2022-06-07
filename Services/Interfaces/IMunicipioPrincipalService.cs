using Domain.Models;
using Domain.Transfer;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IMunicipioPrincipalService
    {
        /// <summary>
        /// Adiciona um Minicipio Principal sobre qual a API vai atuar
        /// </summary>
        public Task<MunicipioPrincipal> AddAsync(MunicipioUpdate municipioUpdate, CancellationToken cancellationToken);

        /// <summary>
        /// Edita o Municipio Principal da API. Inutilizando as Denuncias que foram cadastradas para outro Municipio
        /// </summary>
        public Task<MunicipioPrincipal> EditAsync(MunicipioUpdate municipioUpdate, CancellationToken cancellationToken);

        /// <summary>
        /// Retorna o Municipio Principal da API
        /// </summary>
        public Task<MunicipioPrincipal?> GetAsync(CancellationToken cancellationToken);

    }
}
