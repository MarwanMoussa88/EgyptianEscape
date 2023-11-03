using AutoMapper;
using AutoMapper.QueryableExtensions;
using EgyptianEscape.Application.Repository.IRepository;
using EgyptianEscape.Domain.Data;
using EgyptianEscape.Domain.Entities;
using EgyptianEscape.Domain.Models.Amenity;
using EgyptianEscape.Domain.Models.Villa;
using EgyptianEscape.Domain.Models.VillaNumber;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianEscape.Application.Repository
{
    public class VillaRepository : GenericRepository<Villa>, IVillaRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public VillaRepository(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        public GetVilla GetDetails(int id)
        {
            GetVilla villas = _context.Set<Villa>()
                 .Include(p => p.Amenities)
                 .Where(p => p.Id == id)
                 .ProjectTo<GetVilla>(_mapper.ConfigurationProvider).FirstOrDefault();
                 

            return villas;
        }

        public IEnumerable<GetVilla> GetDetailsAll()
        {
            IEnumerable<GetVilla> villas = _context.Set<Villa>()
              .Include(p=>p.Amenities)
              .ProjectTo<GetVilla>(_mapper.ConfigurationProvider)
              .ToList();
            return villas;
        }
    }
}
