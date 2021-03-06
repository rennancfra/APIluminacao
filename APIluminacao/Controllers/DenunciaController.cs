using APIluminacao.Attributes;
using APIluminacao.ViewModels.Denuncia;
using AutoMapper;
using Domain.Enums;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace APIluminacao.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class DenunciaController : ControllerBase
    {
        private readonly IDenunciaService _denunciaService;
        private readonly IMapper _mapper;
        public DenunciaController(IDenunciaService denunciaService, IMapper mapper)
        {
            _denunciaService = denunciaService;
            _mapper = mapper;
        }

        [HttpPost]
        [HasRolePermission(PermissaoSistemaEnum.DenunciaCria, PermissaoSistemaEnum.UsuarioMaster)]
        public async Task<ActionResult<DenunciaCadastroViewModel>> Add([FromBody] DenunciaCadastroViewModel viewModel, CancellationToken cancellationToken)
        {
            Denuncia entity = this._mapper.Map<Denuncia>(viewModel);
            entity.Finalizado = false;
            Denuncia denunciaAdded = await _denunciaService.AddAsync(entity, cancellationToken);

            return Ok(denunciaAdded);
        }

        [HttpGet]
        [HasRolePermission(PermissaoSistemaEnum.UsuarioMaster)]
        public async Task<ActionResult<Denuncia>> GetId([FromQuery]long codigo, CancellationToken cancellationToken)
        {
            Denuncia denuncia = await this._denunciaService.GetDenunciaAsync(codigo, cancellationToken);

            return Ok(this._mapper.Map<DenunciaCadastroViewModel>(denuncia));
        }

        [HttpPut]
        [HasRolePermission(PermissaoSistemaEnum.DenunciaEdita, PermissaoSistemaEnum.UsuarioMaster)]
        public async Task<ActionResult<Denuncia>> UpdateFinalizado(long codigo, bool finalizado, CancellationToken cancellationToken)
        {
            var denuncia = await this._denunciaService.UpdateStatusDenuncia(codigo, finalizado, cancellationToken);

            return Ok(denuncia);
        }
    }
}
