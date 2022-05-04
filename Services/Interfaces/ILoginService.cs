using Domain.Transfer;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ILoginService
    {
        Task<string> LoginAsync(Login login, CancellationToken cancellationToken);
    }
}
