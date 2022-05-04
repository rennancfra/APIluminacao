using Domain.Enums;
using Domain.Models;
using Domain.Transfer;
using Repository;
using Repository.Interfaces;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Services
{
    public class LoginService : ILoginService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IUsuarioService _usuarioService;
        private readonly IPermissaoUsuarioRepository _permissaoRepository;
        private readonly ITokenService _tokenService;

        public LoginService(IUsuarioRepository usuarioRepository, ITokenService tokenService, IUsuarioService usuarioService, IPermissaoUsuarioRepository permissaoRepository)
        {
            _usuarioRepository = usuarioRepository;
            _tokenService = tokenService;
            _usuarioService = usuarioService;
            _permissaoRepository = permissaoRepository;
        }

        public async Task<string> LoginAsync(Login login, CancellationToken cancellationToken)
        {
            Usuario usuario = await this._usuarioRepository.GetUsuarioByCodigoAsync(login.Usuario, cancellationToken);

            if (!_usuarioService.Authenticate(usuario, login.Senha))
            {
                throw new Exception("Senha inválida.");
            }

            // Retornar Permissões do usuário
            IEnumerable<PermissaoSistemaEnum> permissoes = await this._permissaoRepository.GetPermissionsByUsuarioAsync(usuario.ID!.Value, cancellationToken);

            //Construir o token
            return this._tokenService.BuildTokenUser(usuario, login.ManterLogado, permissoes);
        }
    }
}
