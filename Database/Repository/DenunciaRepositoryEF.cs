using Database.Interface;
using Database.Repository.Common;
using Domain.Models;
using Repository.Interfaces;

namespace Database.Repository
{
    public class DenunciaRepositoryEF : BaseRepositoryEF<Denuncia>, IDenunciaRepository
    {
        public DenunciaRepositoryEF(IEntityContext contexto) : base(contexto)
        {
        }
    }
}
