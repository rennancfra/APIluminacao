namespace Domain.Interface
{
    /// <summary>
    /// Interface que representa o acesso ao usuário logado no sistema
    /// </summary>
    public interface IUsuarioLogado
    {
        /// <summary>
        /// ID do usuário logado
        /// </summary>
        long? ID { get; }

        /// <summary>
        /// Código do usuário logado
        /// </summary>
        string? Codigo { get; }

        /// <summary>
        /// Nome do usuário logado
        /// </summary>
        string? Nome { get; }

        /// <summary>
        /// E-mail do usuário logado
        /// </summary>
        string? Email { get; }

        /// <summary>
        /// Celular do usuário logado
        /// </summary>
        string? Celular { get; }

        /// <summary>
        /// Define se o usuário optou por manter-se logado
        /// </summary>
        bool ManterLogado { get; }
    }
}
