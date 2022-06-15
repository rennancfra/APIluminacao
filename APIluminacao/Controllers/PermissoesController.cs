using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Enums;
using APIluminacao.Attributes;

namespace APIluminacao.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class PermissoesController : ControllerBase
    {
        public PermissoesController()
        {

        }

        [HttpGet]
        [HasRolePermission(PermissaoSistemaEnum.UsuarioMaster)]
        public async Task<ActionResult<PermissaoSistemaEnum>> Get()
        {
            Dictionary<Object, String> permissions = new Dictionary<object, string>()
            {
                [3] = PermissaoSistemaEnum.UsuarioMaster.ToString(),
                [1] = PermissaoSistemaEnum.DenunciaCria.ToString(),
                [1] = PermissaoSistemaEnum.DenunciaEdita.ToString(),
                [5] = PermissaoSistemaEnum.MunicipioCria.ToString(),
                [6] = PermissaoSistemaEnum.MinicipioEdita.ToString(),
                [4] = PermissaoSistemaEnum.UsuarioCria.ToString(),
                [0] = PermissaoSistemaEnum.None.ToString(),
                [-1] = PermissaoSistemaEnum.NaoPermitido.ToString()
            };
            //List<PermissaoSistemaEnum> list = new List<PermissaoSistemaEnum>()
            //{
            //    PermissaoSistemaEnum.UsuarioMaster,
            //    PermissaoSistemaEnum.DenunciaCria,
            //    PermissaoSistemaEnum.DenunciaEdita,
            //    PermissaoSistemaEnum.MunicipioCria,
            //    PermissaoSistemaEnum.MinicipioEdita,
            //    PermissaoSistemaEnum.UsuarioCria,
            //    PermissaoSistemaEnum.None
            //};

            return Ok(permissions);
        }
    }
}
