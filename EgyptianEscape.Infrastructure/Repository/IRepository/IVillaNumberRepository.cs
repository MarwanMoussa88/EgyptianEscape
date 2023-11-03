using EgyptianEscape.Domain.Entities;
using EgyptianEscape.Domain.Models.VillaNumber;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianEscape.Application.Repository.IRepository
{
    public interface IVillaNumberRepository : IGenericRepository<VillaNumber>
    {
        IEnumerable<GetVillaNumber> GetDetails(int id);
        IEnumerable<GetVillaNumber> GetDetailsAll();
    }
}
