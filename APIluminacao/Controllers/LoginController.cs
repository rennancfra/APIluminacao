using APIluminacao.ViewModels.Denuncia;
using APIluminacao.ViewModels.Login;
using AutoMapper;
using Domain.Transfer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace APIluminacao.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly Services.Interfaces.IAuthorizationService _authorizationService;
        private readonly ILoginService _loginService;
        private readonly IMapper _mapper;
        public LoginController(Services.Interfaces.IAuthorizationService authorizationService, IMapper mapper, ILoginService loginService)
        {
            _authorizationService = authorizationService;
            _mapper = mapper;
            _loginService = loginService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<DenunciaCadastroViewModel>> Login([FromBody] LoginViewModel viewModel, CancellationToken cancellationToken)
        {
            Login login = this._mapper.Map<Login>(viewModel);

            string tokenUser = await _loginService.LoginAsync(login, cancellationToken);

            return Ok(tokenUser);
        }
    }
}
