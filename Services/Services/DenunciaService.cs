using Domain.Exceptions;
using Domain.Interface;
using Domain.Models;
using Repository;
using Services.Interfaces;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using ViaCep;

namespace Services.Services
{
    public class DenunciaService : IDenunciaService
    {
        private readonly IUnitOfWork _uow;
        private readonly IViaCepClient _viaCep;
        private readonly ICurrentUser _currentUser;

        public DenunciaService(IUnitOfWork uow, IViaCepClient viaCep, ICurrentUser currentUser)
        {
            _uow = uow;
            _viaCep = viaCep;
            _currentUser = currentUser;
        }

        public async Task<Denuncia> AddAsync(Denuncia denuncia, CancellationToken cancellationToken)
        {
            try
            {
                await this.ValidateDenunciaAsync(denuncia, cancellationToken);

                // Atribui o usuário logado à Denuncia
                denuncia.UsuarioID = this._currentUser.ID!.Value;

                await this._uow.DenunciaRepository.AddAsync(denuncia, cancellationToken);

                await _uow.ApplyChangesAsync(cancellationToken);

                return denuncia;
            }
            catch (ViaCepException)
            {
                throw;
            }
            catch (Exception exc)
            {
                throw new Exception("Ocorreu um erro durante o cadastro da Denuncia.", exc);
            }

        }

        public async Task<Denuncia> GetDenunciaAsync(long codigo, CancellationToken cancellationToken)
        {
            return await this._uow.DenunciaRepository.GetDenunciaId(codigo, cancellationToken);
        }

        public async Task<Denuncia> UpdateStatusDenuncia(long codigo, bool finalizado, CancellationToken cancellationToken)
        {
            var pegarDenuncia = await GetDenunciaAsync(codigo, cancellationToken);
            pegarDenuncia.Finalizado = finalizado;
            this._uow.DenunciaRepository.Update(pegarDenuncia);
            await _uow.ApplyChangesAsync(cancellationToken);

            return pegarDenuncia;
        }

        #region Auxiliares
        private async Task ValidateDenunciaAsync(Denuncia denuncia, CancellationToken cancellationToken)
        {
            // Se o CEP não for válido, não permite a ação
            if (string.IsNullOrEmpty(denuncia.CEP) || !ValidaCEP(denuncia.CEP))
            {
                throw new Exception("O CEP informado não é válido.");
            }

            if (string.IsNullOrEmpty(denuncia.Rua) || denuncia.Numero == 0)
            {
                throw new Exception("É obrigatório informar uma Rua e Número.");
            }

            // Qualquer erro no ViaCEP, não permite a ação
            ViaCepResult? result = await this._viaCep.SearchAsync(denuncia.CEP, cancellationToken);
            if (result == null)
            {
                throw new ViaCepException("Não foi possível encontrar uma localização com base no CEP fornecido. Verifique os dados e tente novamente.");
            }

            // Se não há municipio de atuação definido, não permite a ação
            MunicipioPrincipal? municipioPrincipal = (await this._uow.MunicipioPrincipalRepository.GetAllAsync(cancellationToken)).FirstOrDefault();
            if (municipioPrincipal == null)
            {
                throw new ViaCepException("A API ainda não possui o MunicipioPrincipal de atuação, por favor, defina primeiro e tente novamente.");
            }

            // Se a Denúncia não estiver dentro do MunicipioPrincipal, está inválida
            if (municipioPrincipal.Nome !=  result.City)
            {
                throw new ViaCepException(string.Format("O CEP informado não corresponde ao Municipio que a API está atuando. Informe um CEP dentro de {0} e tente novamente.", municipioPrincipal.Nome));
            }
        }

        public bool ValidaCEP(string cep)
        {
            //Regex Rgx = new(@"^\d{5}-\d{3}$");
            Regex Rgx = new(@"^\d{8}$");

            if (!Rgx.IsMatch(cep))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion
    }
}
