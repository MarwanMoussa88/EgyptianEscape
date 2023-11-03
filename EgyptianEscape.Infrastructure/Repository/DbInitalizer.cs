using EgyptianEscape.Application.Utility;
using EgyptianEscape.Domain.Data;
using EgyptianEscape.Domain.Entities;
using EgyptianEscape.Domain.Models.ApplicationUser;
using EgyptianEscape.Infrastructure.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianEscape.Infrastructure.Repository
{
    public class DbInitalizer : IDbInitalizer
    {
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public DbInitalizer(ApplicationDbContext context,RoleManager<IdentityRole> roleManager,UserManager<ApplicationUser> userManager)
        {
            this._context = context;
            this._roleManager = roleManager;
            this._userManager = userManager;
        }
        public async  Task Initalize()
        {
            try
            {
                if((await _context.Database.GetPendingMigrationsAsync()).Count()>0)
                {
                    await _context.Database.MigrateAsync();
                    await CreateRoles();
                }
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public async Task CreateRoles()
        {
            await _roleManager.CreateAsync(new IdentityRole(SD.Role_Admin));
            await _roleManager.CreateAsync(new IdentityRole(SD.Role_Customer));
            await CreateInitialAdminUser();
        }
        public async Task CreateInitialAdminUser()
        {
           ApplicationUser user =new ApplicationUser
            {
                UserName = "test@gmail.com",
                Email = "test@gmail.com",
                Name = "Marwan Moussa",
                NormalizedUserName = "TEST@GMAIL.COM",
                NormalizedEmail = "TEST@GMAIL.COM",
                PhoneNumber = "123456789"  
            };
            await _userManager.CreateAsync(user, "Admin123-_-");

            ApplicationUser adminUser=await _userManager.FindByEmailAsync(user.Email);
            await _userManager.AddToRoleAsync(adminUser, SD.Role_Admin);


            
        }

    }
}
