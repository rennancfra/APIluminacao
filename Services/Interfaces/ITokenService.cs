using Domain.Enums;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    /// <summary>
    /// Serviço de geração do token para autenticação do sistema
    /// </summary>
    public interface ITokenService
    {
        /// <summary>
        /// Cria um token de autenticação para o usuário com as permissões de usuário especificadas
        /// </summary>
        string BuildTokenUser(Usuario usuario, bool manterLogado, IEnumerable<PermissaoSistemaEnum> permissoes);
    }

}
