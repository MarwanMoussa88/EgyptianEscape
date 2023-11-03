using EgyptianEscape.Domain.Entities;
using EgyptianEscape.Domain.Models.ApplicationUser;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianEscape.Infrastructure.Repository.IRepository
{
    public interface IAuthManager
    {
        Task<IEnumerable<IdentityError>> RegisterUser(CreateApplicationUser user,string role);
        Task<bool> LoginUser(GetApplicationUser loginUser, bool rememberMe);
        Task Logout();

        bool CheckNullOrIsEmpty(string redirectUrl);

        Task<bool> AreRolesCreated();

        void CreateRoles();
        Task<IEnumerable<IdentityRole>> GetRoles();
    }
}
