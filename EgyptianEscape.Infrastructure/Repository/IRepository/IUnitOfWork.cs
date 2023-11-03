using EgyptianEscape.Infrastructure.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianEscape.Application.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IVillaNumberRepository VillaNumberRepository { get; }
        IVillaRepository VillaRepository { get; }
        IAmenityRepository AmenityRepository { get; }
        IAuthManager AuthManager { get; }

        IUserRepository UserRepository { get; }

        IBookingRepository BookingRepository { get; }
        
    }
}
