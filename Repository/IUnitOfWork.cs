using Repository.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Repository
{
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Testa a conexão do contexto padrão
        /// </summary>
        void ConnectionTestDefault();

        /// <summary>
        /// Aplica as mudanças realizadas nos objetos para o banco de dados
        /// </summary>
        Task ApplyChangesAsync(CancellationToken cancellationToken);

        #region Repositórios Principais
        IDenunciaRepository DenunciaRepository { get; }
        IUsuarioRepository UsuarioRepository { get; }
        #endregion
    }
}
