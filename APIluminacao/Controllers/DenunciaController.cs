using APIluminacao.ViewModels.Denuncia;
using AutoMapper;
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
        public async Task<ActionResult<DenunciaCadastroViewModel>> Add([FromBody] DenunciaCadastroViewModel viewModel, CancellationToken cancellationToken)
        {
            Denuncia entity = this._mapper.Map<Denuncia>(viewModel);

            Denuncia denunciaAdded = await _denunciaService.AddAsync(entity, cancellationToken);

            return Ok(denunciaAdded);
        }
    }
}
