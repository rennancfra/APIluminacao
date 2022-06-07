using Domain.Exceptions;
using Domain.Models;
using Domain.Transfer;
using Repository;
using Services.Interfaces;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ViaCep;

namespace Services.Services
{
    public class MunicipioPrincipalService : IMunicipioPrincipalService
    {
        private readonly IUnitOfWork _uow;
        private readonly IViaCepClient _viaCepClient;

        public MunicipioPrincipalService(IUnitOfWork uow, IViaCepClient viaCepClient)
        {
            _uow = uow;
            _viaCepClient = viaCepClient;
        }

        public async Task<MunicipioPrincipal> AddAsync(MunicipioUpdate municipioUpdate, CancellationToken cancellationToken)
        {
            try
            {
                ViaCepResult? result = await this._viaCepClient.SearchAsync(municipioUpdate.CEP, cancellationToken);

                if (result == null)
                {
                    throw new ViaCepException("Não foi possível encontrar uma localização com base no CEP fornecido. Verifique os dados e tente novamente.");
                }

                var municipios = await this._uow.MunicipioPrincipalRepository.GetAllAsync(cancellationToken);

                if (municipios.Any())
                {
                    throw new ViaCepException("Já existe um Municipio configurado para a API.");
                }

                MunicipioPrincipal municipioCreate = new()
                {
                    Nome = result.City,
                    UF = result.StateInitials
                };

                await this._uow.MunicipioPrincipalRepository.AddAsync(municipioCreate, cancellationToken);

                await this._uow.ApplyChangesAsync(cancellationToken);

                return municipioCreate;
            }
            catch (ViaCepException)
            {
                throw;
            }
            catch (Exception exc)
            {
                throw new Exception("Ocorreu um erro durante a criação do MunicipioPrincipal. Aguarde alguns segundos e tente novamente.", exc);
            }
        }

        public async Task<MunicipioPrincipal> EditAsync(MunicipioUpdate municipioUpdate, CancellationToken cancellationToken)
        {
            try
            {
                ViaCepResult? result = await this._viaCepClient.SearchAsync(municipioUpdate.CEP, cancellationToken);

                if (result == null)
                {
                    throw new ViaCepException("Não foi possível encontrar uma localização com base no CEP fornecido. Verifique os dados e tente novamente.");
                }

                MunicipioPrincipal municipioEdit = new()
                {
                    Nome = result.City,
                    UF = result.StateInitials
                };

                this._uow.MunicipioPrincipalRepository.Update(municipioEdit);

                await this._uow.ApplyChangesAsync(cancellationToken);

                return municipioEdit;
            }
            catch (ViaCepException)
            {
                throw;
            }
            catch (Exception exc)
            {
                throw new Exception("Ocorreu um erro durante a edição do MunicipioPrincipal. Aguarde alguns segundos e tente novamente.", exc);
            }
        }

        public async Task<MunicipioPrincipal?> GetAsync(CancellationToken cancellationToken)
        {
            return (await this._uow.MunicipioPrincipalRepository.GetAllAsync(cancellationToken)).FirstOrDefault();
        }
    }
}
