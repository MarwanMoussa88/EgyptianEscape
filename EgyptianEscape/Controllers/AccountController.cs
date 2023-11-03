using AutoMapper;
using EgyptianEscape.Application.Repository.IRepository;
using EgyptianEscape.Application.Utility;
using EgyptianEscape.Domain.Entities;
using EgyptianEscape.Domain.Models.ApplicationUser;
using EgyptianEscape.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EgyptianEscape.Controllers
{
    public class AccountController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountController(IUnitOfWork unitOfWork, RoleManager<IdentityRole> roleManager,UserManager<ApplicationUser> userManager)
        {

            this._unitOfWork = unitOfWork;
            this._roleManager = roleManager;
            this._userManager = userManager;
        }
        public IActionResult Login(string returnUrl = null)
        {
            //assign url if null 
            returnUrl ??= Url.Content("~/");
            LoginVM vm = new LoginVM
            {
                RedirectUrl = returnUrl,

            };
            return View(vm);
        }
        public  async Task<IActionResult> Register(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            RegisterVM vm = new RegisterVM
            {
                RedirectUrl= returnUrl,
                Roles = (await _unitOfWork.AuthManager.GetRoles()).Select(x =>
                    new SelectListItem { Text = x.Name, Value = x.Name.ToString() })
            };

            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {

            if (ModelState.IsValid)
            {
                var errors = await _unitOfWork.AuthManager.RegisterUser(registerVM.CreateApplicationUser, registerVM.Role);

                if (errors.Count() > 0)
                {
                    foreach (var error in errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    registerVM.Roles = (await _unitOfWork.AuthManager.GetRoles()).Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Name.ToString(),
                    });
                    return View(registerVM);
                }
            }

            if (_unitOfWork.AuthManager.CheckNullOrIsEmpty(registerVM.RedirectUrl))
                return RedirectToAction("Index", "Home");

            else
                return LocalRedirect(registerVM.RedirectUrl);


        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVm)
        {

             var isLogin = await _unitOfWork.AuthManager.LoginUser(loginVm.getApplicationUser,loginVm.RememberMe);

            if(!isLogin)    
                return View(loginVm);

            var user = await _userManager.FindByEmailAsync(loginVm.getApplicationUser.Email);

            if (await _userManager.IsInRoleAsync(user, SD.Role_Admin))
            {
                return RedirectToAction("Index", "Dashboard");
            }

            if (_unitOfWork.AuthManager.CheckNullOrIsEmpty(loginVm.RedirectUrl))
                    return RedirectToAction("Index", "Home");


            else
                return LocalRedirect(loginVm.RedirectUrl);

        }
        public async Task<IActionResult> SignOut()
        {
            await _unitOfWork.AuthManager.Logout();
            return RedirectToAction("Index","Home");
        }

        public async Task<IActionResult> AccessDenied()
        {
            
            return View();
        }
    }
}
