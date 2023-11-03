using AutoMapper;
using AutoMapper.QueryableExtensions;
using EgyptianEscape.Application.Repository.IRepository;
using EgyptianEscape.Domain.Data;
using EgyptianEscape.Domain.Entities;
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
    internal class VillaNumberRepository : GenericRepository<VillaNumber>, IVillaNumberRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public VillaNumberRepository(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        public IEnumerable<GetVillaNumber> GetDetails(int id)
        {
            var villas = _context.Set<VillaNumber>()
                 .Include(p => p.Villa)
                 .Where(p => p.VillaId == id)
                 .ProjectTo<GetVillaNumber>(_mapper.ConfigurationProvider)
                 .ToList();


            return villas;
        }

        public IEnumerable<GetVillaNumber> GetDetailsAll()
        {
            var villas = _context.Set<VillaNumber>()
                .Include(p => p.Villa)
                .ProjectTo<GetVillaNumber>(_mapper.ConfigurationProvider)
                .ToList();

            return villas;
        }
    }
}
