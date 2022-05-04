using Domain.Models;

namespace Services.Interfaces
{
    public interface IUsuarioService
    {
        bool Authenticate(Usuario usuario, string senha);
    }
}
