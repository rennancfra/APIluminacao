using Domain.Core;
using System;

namespace Domain.Models
{
    public class Denuncia : Entity<Denuncia>
    {
        public string Descricao { get; set; } = null!;
        public long UsuarioID { get; set; }
        public string CEP { get; set; } = null!;
        public string Rua { get; set; } = null!;
        public int Numero { get; set; } = 0!;
        public Usuario? Usuario { get; set; } = null!;
    }
}
