using EgyptianEscape.Application.Repository;
using EgyptianEscape.Application.Repository.IRepository;
using EgyptianEscape.Domain.Entities;
using EgyptianEscape.Domain.Models.Amenity;
using EgyptianEscape.Domain.Models.VillaNumber;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianEscape.Infrastructure.Repository.IRepository
{
    public interface IAmenityRepository : IGenericRepository<Amenity>
    {
        IEnumerable<GetAmenity> GetDetails(int id);
        IEnumerable<GetAmenity> GetDetailsAll();
    }
}
