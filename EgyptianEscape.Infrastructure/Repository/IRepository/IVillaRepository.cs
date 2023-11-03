using EgyptianEscape.Domain.Entities;
using EgyptianEscape.Domain.Models.Villa;
using EgyptianEscape.Domain.Models.VillaNumber;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianEscape.Application.Repository.IRepository
{
    public interface IVillaRepository : IGenericRepository<Villa>
    {
        IEnumerable<GetVilla> GetDetailsAll();
        public GetVilla GetDetails(int id);
    }
}
