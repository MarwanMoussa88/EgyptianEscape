using AutoMapper;
using EgyptianEscape.Application.Repository;
using EgyptianEscape.Domain.Data;
using EgyptianEscape.Domain.Entities;
using EgyptianEscape.Infrastructure.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianEscape.Infrastructure.Repository
{
    public class UserRepository : GenericRepository<ApplicationUser>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
