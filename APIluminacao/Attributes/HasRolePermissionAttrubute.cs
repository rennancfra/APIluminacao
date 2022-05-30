using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Linq;

namespace APIluminacao.Attributes
{
    /// <summary>
    ///  Atributo para padronização da utilização de Permissoes no AuthorizeAttribute.
    ///  Realiza a conversão do Enum recebido e adiciona os valores para restrição de autorização por Roles.
    ///  A utilização deste atributo segue a mesma especificada na documentação para o AuthorizeAttribute
    ///  Para combinação "OU" enviar array de PermissaoSistemaEnum, e para combinação "E" adicionar nova anotação do atributo
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public class HasRolePermissionAttribute : AuthorizeAttribute
    {
        public HasRolePermissionAttribute(params PermissaoSistemaEnum[] permissoes) : base("Bearer")
        {
            // concatena permissões recebidas
            if (permissoes.Any())
            {
                this.Roles = FormataRoles(permissoes);
            }
        }

        /// <summary>
        /// A partir de um array de PermissaoSistemaEnum retorna valores em formato aceito para utilização na propriedade Role
        /// </summary>
        /// <param name="permissoes"></param>
        /// <returns></returns>
        private string FormataRoles(PermissaoSistemaEnum[] permissoes)
        {
            return string.Join(",", permissoes.Select(p => (int)p));
        }
    }
}
