using EgyptianEscape.Application.Repository;
using EgyptianEscape.Application.Repository.IRepository;
using EgyptianEscape.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianEscape.Infrastructure.Repository.IRepository
{
    public interface IUserRepository : IGenericRepository<ApplicationUser>
    {
    }
}
