using AutoMapper;
using AutoMapper.QueryableExtensions;
using EgyptianEscape.Application.Repository;
using EgyptianEscape.Domain.Data;
using EgyptianEscape.Domain.Entities;
using EgyptianEscape.Domain.Models.Amenity;
using EgyptianEscape.Domain.Models.VillaNumber;
using EgyptianEscape.Infrastructure.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianEscape.Infrastructure.Repository
{
    internal class AmenityRepository : GenericRepository<Amenity>,IAmenityRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public AmenityRepository(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        public IEnumerable<GetAmenity> GetDetails(int id)
        {
            var amenities = _context.Set<Amenity>()
                 .Include(p => p.Villa)
                 .Where(p => p.VillaId == id)
                 .ProjectTo<GetAmenity>(_mapper.ConfigurationProvider)
                 .ToList();


            return amenities;
        }

        public IEnumerable<GetAmenity> GetDetailsAll()
        {
            var amenities = _context.Set<Amenity>()
                .Include(p => p.Villa)
                .ProjectTo<GetAmenity>(_mapper.ConfigurationProvider)
                .ToList();

            return amenities;
        }
    }
}
