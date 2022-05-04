using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    /// <summary>
    /// Enum para definição das claims customizadas do sistema
    /// </summary>
    public enum CustomClaimTypeEnum
    {
        /// <summary>
        /// Identificação de segurança do usuário logado
        /// </summary>
        Sid,

        /// <summary>
        /// Identificação do usuário logado
        /// </summary>
        NameIdentifier,

        /// <summary>
        /// Nome do usuário logado
        /// </summary>
        Name,

        /// <summary>
        /// Endereço Eletrônico do usuário logado
        /// </summary>
        Email,

        /// <summary>
        /// Número de Telefone do Usuário logado
        /// </summary>
        MobilePhone,

        /// <summary>
        /// Identifica se o usuário optou por manter-se logado
        /// </summary>
        ManterLogado,

        /// <summary>
        /// Identificação das Permissões do Usuario logado
        /// </summary>
        Role,
    }
}
