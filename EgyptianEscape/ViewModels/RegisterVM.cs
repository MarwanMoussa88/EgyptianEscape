using EgyptianEscape.Domain.Models.ApplicationUser;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace EgyptianEscape.ViewModels
{
    public class RegisterVM
    {
        public CreateApplicationUser CreateApplicationUser { get; set; }
        public String? RedirectUrl { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> Roles { get; set; }

        public string? Role { get; set; }
    }
}
