using Database.Interface;
using Database.Repository.Common;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Database.Repository
{
    public class MunicipioPrincipalRepositoryEF : BaseRepositoryEF<MunicipioPrincipal>, IMunicipioPrincipalRepository
    {
        public MunicipioPrincipalRepositoryEF(IEntityContext contexto) : base(contexto)
        {
        }
    }
}
