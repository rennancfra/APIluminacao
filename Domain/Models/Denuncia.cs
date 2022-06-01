using Domain.Core;
using System;

namespace Domain.Models
{
    public class Denuncia : Entity<Denuncia>
    {
        public string Descricao { get; set; } = null!;
        public long UsuarioId { get; set; }
        public string Cep { get; set; } = null!;
        public int Numero { get; set; } = 0!;
    }
}
