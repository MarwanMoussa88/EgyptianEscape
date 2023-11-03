using AutoMapper;
using EgyptianEscape.Application.Repository.IRepository;
using EgyptianEscape.Domain.Data;
using EgyptianEscape.Domain.Entities;
using EgyptianEscape.Infrastructure.Repository;
using EgyptianEscape.Infrastructure.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianEscape.Application.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;

        public IVillaNumberRepository VillaNumberRepository { get; private set; }

        public IVillaRepository VillaRepository { get; private set; }

        public IAmenityRepository AmenityRepository { get; private set; }

        public IAuthManager AuthManager { get; private set; }

        public IBookingRepository BookingRepository { get; private set; }

        public IUserRepository UserRepository { get; private set; }

        public UnitOfWork(
            ApplicationDbContext context, 
            IMapper mapper, 
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<ApplicationUser> signInManager,IConfiguration configuration)
        {
            this._context = context;
            this._mapper = mapper;
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._signInManager = signInManager;
            this._configuration = configuration;
            VillaNumberRepository = new VillaNumberRepository(_context, _mapper);
            UserRepository = new UserRepository(_context, _mapper);
            VillaRepository = new VillaRepository(_context, _mapper);
            AmenityRepository = new AmenityRepository(context, _mapper);
            AuthManager = new AuthManager(
                _mapper, _context, _userManager, _roleManager, _signInManager, _configuration);

            BookingRepository=new BookingRepository(context, _mapper);
            

        }

    }
}
