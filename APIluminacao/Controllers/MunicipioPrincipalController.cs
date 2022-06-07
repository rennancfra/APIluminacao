using APIluminacao.Attributes;
using APIluminacao.ViewModels.Denuncia;
using APIluminacao.ViewModels.Municipio;
using AutoMapper;
using Domain.Enums;
using Domain.Models;
using Domain.Transfer;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace APIluminacao.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class MunicipioPrincipalController : ControllerBase
    {
        private readonly IMunicipioPrincipalService _municipioPrincipalService;
        private readonly IMapper _mapper;
        public MunicipioPrincipalController(IMunicipioPrincipalService municipioPrincipalService, IMapper mapper)
        {
            _municipioPrincipalService = municipioPrincipalService;
            _mapper = mapper;
        }

        [HttpPost]
        [HasRolePermission(PermissaoSistemaEnum.UsuarioMaster, PermissaoSistemaEnum.MunicipioCria)]
        public async Task<ActionResult<DenunciaCadastroViewModel>> Add([FromBody] MunicipioUpdateViewModel viewModel, CancellationToken cancellationToken)
        {
            MunicipioUpdate entity = this._mapper.Map<MunicipioUpdate>(viewModel);

            MunicipioPrincipal municipioAdded = await _municipioPrincipalService.AddAsync(entity, cancellationToken);

            return Ok(municipioAdded);
        }

        [HttpPost]
        [HasRolePermission(PermissaoSistemaEnum.UsuarioMaster, PermissaoSistemaEnum.MunicipioCria)]
        public async Task<ActionResult<DenunciaCadastroViewModel>> Edit([FromBody] MunicipioUpdateViewModel viewModel, CancellationToken cancellationToken)
        {
            MunicipioUpdate entity = this._mapper.Map<MunicipioUpdate>(viewModel);

            MunicipioPrincipal municipioAdded = await _municipioPrincipalService.EditAsync(entity, cancellationToken);

            return Ok(municipioAdded);
        }

        [HttpGet]
        [HasRolePermission(PermissaoSistemaEnum.UsuarioMaster)]
        public async Task<ActionResult<MunicipioCadastroViewModel>> Get(CancellationToken cancellationToken)
        {
            MunicipioPrincipal? municipioPrincipal = await _municipioPrincipalService.GetAsync(cancellationToken);

            if (municipioPrincipal == null)
            {
                return NotFound("A API ainda não possui um Municipio de atuação.");
            }

            return Ok(this._mapper.Map<MunicipioCadastroViewModel>(municipioPrincipal));
        }
    }
}
