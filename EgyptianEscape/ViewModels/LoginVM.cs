using EgyptianEscape.Domain.Models.ApplicationUser;
using System.ComponentModel.DataAnnotations;

namespace EgyptianEscape.ViewModels
{
    public class LoginVM
    {
        
        public GetApplicationUser getApplicationUser { get; set; }
        public string? RedirectUrl { get; set; }
        public bool RememberMe { get; set; }



    }
}
