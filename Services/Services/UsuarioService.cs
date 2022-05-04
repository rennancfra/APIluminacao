using Domain.Models;
using Domain.Transfer;
using Repository;
using Repository.Interfaces;
using Services.Extensions;
using Services.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Services
{
    public class UsuarioService : IUsuarioService
    {
        public UsuarioService()
        {
        }

        public bool Authenticate(Usuario usuario, string senha)
        {
            // Pega a senha criptografada atual esperada para o usuário
            string? senhaAtual = usuario.Senha;
            string? hashAtual = usuario.Hash;

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
    }
}
