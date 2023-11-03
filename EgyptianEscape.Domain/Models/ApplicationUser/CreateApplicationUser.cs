using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianEscape.Domain.Models.ApplicationUser
{
    public class CreateApplicationUser:BaseApplicationUser
    {
        [Required]
        public string? Name { get; set; }

        [Required]
        [Display(Name="Phone Number")]
        public string? PhoneNumber { get; set; }

 
    }
}
