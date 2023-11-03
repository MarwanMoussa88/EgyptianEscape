using AutoMapper;
using EgyptianEscape.Domain.Entities;
using EgyptianEscape.Domain.Models.Amenity;
using EgyptianEscape.Domain.Models.ApplicationUser;
using EgyptianEscape.Domain.Models.Booking;
using EgyptianEscape.Domain.Models.Villa;
using EgyptianEscape.Domain.Models.VillaNumber;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianEscape.Infrastructure.Automapper
{
    public class AutomapperConfiguration : Profile
    {
        public AutomapperConfiguration()
        {

            CreateMap<IEnumerable<Villa>, IEnumerable<GetVilla>>().ReverseMap();
            CreateMap<Villa, GetVilla>().ReverseMap();
            CreateMap<Villa, UpdateVilla>().ReverseMap();
            CreateMap<Villa, CreateVilla>().ReverseMap();
            CreateMap<VillaNumber, GetVillaNumber>().ReverseMap();
            CreateMap<VillaNumber, UpdateVillaNumber>().ReverseMap();
            CreateMap<VillaNumber, CreateVillaNumber>().ReverseMap();
            CreateMap<Amenity, GetAmenity>().ReverseMap();
            CreateMap<Amenity, UpdateAmenity>().ReverseMap();
            CreateMap<Amenity, CreateAmenity>().ReverseMap();
            CreateMap<ApplicationUser, CreateApplicationUser>().ReverseMap();
            CreateMap<ApplicationUser, GetApplicationUser>().ReverseMap();
            CreateMap<ApplicationUser, UpdateApplicationUser>().ReverseMap();
            CreateMap<Booking, CreateBooking>().ReverseMap();
            CreateMap<Booking, GetBooking>().ReverseMap();
            CreateMap<Booking, UpdateBooking>().ReverseMap();

        }
    }
}
