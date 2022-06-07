using APIluminacao.Attributes;
using APIluminacao.ViewModels.Usuario;
using AutoMapper;
using Domain.Enums;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace APIluminacao.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IMapper _mapper;
        public UsuarioController(IMapper mapper, IUsuarioService usuarioService)
        {
            _mapper = mapper;
            _usuarioService = usuarioService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<UsuarioCadastroViewModel>> Create([FromBody] UsuarioCadastroViewModel viewModel, CancellationToken cancellationToken)
        {
            Usuario entity = this._mapper.Map<Usuario>(viewModel);

            Usuario usuarioAdded = await _usuarioService.CreateAsync(entity, cancellationToken);

            return Ok(usuarioAdded);
        }

        [HttpGet]
        [HasRolePermission(PermissaoSistemaEnum.UsuarioMaster)]
        public async Task<ActionResult<IEnumerable<UsuarioCadastroViewModel>>> GetAll(CancellationToken cancellationToken)
        {
            IEnumerable<Usuario> usuarios = await _usuarioService.GetAllAsync(cancellationToken);

            return Ok(usuarios.Select(c => this._mapper.Map<UsuarioCadastroViewModel>(c)));
        }
    }
}
