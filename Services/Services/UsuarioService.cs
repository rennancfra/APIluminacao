using Domain.Models;
using Domain.Transfer;
using Repository;
using Repository.Interfaces;
using Services.Extensions;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUnitOfWork _uow;

        public UsuarioService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Usuario> CreateAsync(Usuario usuario, CancellationToken cancellationToken)
        {
            // Valida se o Usuário pode ser cadastrado
            await this.ValidateCreate(usuario, cancellationToken);

            try
            {
                // Verifica se o usuário já possui um Hash no formato GUID, e se não possuir cria um novo
                if (!Guid.TryParse(usuario.Hash, out var newHash))
                {
                    usuario.Hash = Guid.NewGuid().ToString();
                }

                // Realiza o Encrypt da Senha do usuário com base no Hash criado
                usuario.Senha = this.EncryptPassword(usuario.Senha!, usuario.Hash);

                // Cria o usuário na base de dados
                await this._uow.UsuarioRepository.AddAsync(usuario, cancellationToken);

                // Confirma as alterações na base de dados
                await this._uow.ApplyChangesAsync(cancellationToken);

                // Retorna o usuário criado
                return usuario;
            }
            catch (Exception exc)
            {
                throw new Exception(string.Format("Ocorreu um erro durante o cadastro de Usuário. Verifique os dados informados e tente novamente. \n Detalhes: {0}"), exc);
            }
        }

        public async Task<IEnumerable<Usuario>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await this._uow.UsuarioRepository.GetAllAsync(cancellationToken);
        }

        #region Auxiliares
        /// <summary>
        /// Retorna se o o Usuário pode ser criado
        /// </summary>
        private async Task ValidateCreate(Usuario usuario, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(usuario.Codigo))
            {
                throw new Exception("O Código informado não pode ser nulo ou vazio.");
            }

            if (await this._uow.UsuarioRepository.ExistsUsuarioByCodigo(usuario.Codigo, cancellationToken))
            {
                throw new Exception(string.Format("O Usuário: {0} já existe no sistema. Informe um código diferente e tente novamente.", usuario.Codigo));
            }

            if (string.IsNullOrEmpty(usuario.Nome))
            {
                throw new Exception("O Nome informado não pode ser nulo ou vazio.");
            }

            if (string.IsNullOrEmpty(usuario.Senha))
            {
                throw new Exception("A Senha informado não pode ser nulo ou vazio.");
            }
        }

        public bool Authenticate(Usuario usuario, string senha)
        {
            // Pega a senha criptografada atual esperada para o usuário
            string? senhaAtual = usuario?.Senha ?? "";
            string? hashAtual = usuario?.Hash ?? "";

            // Processa a senha com o algorítmo de criptografia para verificar a senha
            string senhaInserida = this.EncryptPassword(senha, hashAtual!);

            // Retorna sucesso se a senha inserida resultou no mesmo cálculo que a esperada
            return senhaAtual == senhaInserida;
        }

        public string EncryptPassword(string password, string hash)
        {
            // Concatena a senha com mais algumas informações
            string passwordPlus = password + hash + password.Length;

            // Criptografa o texto definido
            string passwordSHA256 = passwordPlus.CryptographySHA256();

            // Segunda iteração para aumentar a complexidade do hash da senha
            passwordSHA256 = (passwordSHA256 + hash + passwordSHA256.Length).CryptographySHA256();

            // Retorna a senha calculadas
            return passwordSHA256;
        }

        #endregion
    }
}
