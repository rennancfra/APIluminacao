using Domain.Core;

namespace Domain.Models
{
    public class Denuncia : Entity<Denuncia>
    {
        public string Descricao { get; set; } = null!;
    }
}
