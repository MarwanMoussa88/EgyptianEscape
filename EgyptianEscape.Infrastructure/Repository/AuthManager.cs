using AutoMapper;
using EgyptianEscape.Application.Repository;
using EgyptianEscape.Application.Utility;
using EgyptianEscape.Domain.Data;
using EgyptianEscape.Domain.Entities;
using EgyptianEscape.Domain.Models.ApplicationUser;
using EgyptianEscape.Infrastructure.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianEscape.Infrastructure.Repository
{
    public class AuthManager : IAuthManager
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;
        private ApplicationUser _user;

        public AuthManager(
            IMapper mapper,
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<ApplicationUser> signInManager, IConfiguration configuration)
        {
            this._mapper = mapper;
            this._context = context;
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._signInManager = signInManager;
            this._configuration = configuration;
        }

        public async Task<bool> AreRolesCreated()
        {
            return await _roleManager.RoleExistsAsync(SD.Role_Admin);
        }

        public bool CheckNullOrIsEmpty(string text)
        {
            return string.IsNullOrEmpty(text);
        }

        public async void CreateRoles()
        {
            await _roleManager.CreateAsync(new IdentityRole(SD.Role_Admin));
            await _roleManager.CreateAsync(new IdentityRole(SD.Role_Customer));
        }

        public async Task<IEnumerable<IdentityRole>> GetRoles()
        {
            if (await AreRolesCreated())
                return _roleManager.Roles;
            else
            {
                CreateRoles();
                return _roleManager.Roles;
            }
        }

        public async Task<bool> LoginUser(GetApplicationUser loginUser, bool rememberMe)
        {

            loginUser.Username = loginUser.Email;
            _user = _mapper.Map<ApplicationUser>(loginUser);
            

            var result = await _signInManager
                .PasswordSignInAsync(loginUser.Email, loginUser.Password, rememberMe, false);

            if (result.Succeeded)
            {
                var token = GenerateToken();
                return true;
            }

            return false;



        }
        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<IEnumerable<IdentityError>> RegisterUser(CreateApplicationUser createUser, string role)
        {
            createUser.Username = createUser.Email;
            _user = _mapper.Map<ApplicationUser>(createUser);
            _user.CreatedAt = DateTime.Now;
            var result = await _userManager.CreateAsync(_user, createUser.Password);
            if (result.Succeeded)
            {
                if (!CheckNullOrIsEmpty(role))
                    await _userManager.AddToRoleAsync(_user, role);
                else
                    await _userManager.AddToRoleAsync(_user, SD.Role_Customer);

                await _signInManager.SignInAsync(_user, isPersistent: false);
            }


            return result.Errors;

        }

        private async Task<string> GenerateToken()
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var roles = await _userManager.GetRolesAsync(_user);
            var roleClaims = roles.Select(x => new Claim(ClaimTypes.Role, x));
            var userClaims = await _userManager.GetClaimsAsync(_user);
            var claims = new List<Claim>
            {
                //Subject of the token
                new Claim(JwtRegisteredClaimNames.Sub,_user.Email),
                //Unique Identifer for the token
                new Claim(JwtRegisteredClaimNames.Jti,new Guid().ToString()),
                //User's Email
                new Claim(JwtRegisteredClaimNames.Email,_user.Email),
                //FirstName
                new Claim(JwtRegisteredClaimNames.Name,_user.Name),
                //User Id
                new Claim("UID",_user.Id)

            }.Union(userClaims).Union(roleClaims);

            var token = new JwtSecurityToken
            (
                issuer: _configuration["JwtSettings:issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: credentials

            );  

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
