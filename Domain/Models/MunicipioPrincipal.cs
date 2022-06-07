using Domain.Core;
using System.Collections.Generic;

namespace Domain.Models
{
    public class MunicipioPrincipal : Entity<MunicipioPrincipal>
    {
        public string Nome { get; set; } = null!;
        public string UF { get; set; } = null!;
    }
}
