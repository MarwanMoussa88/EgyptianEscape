using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianEscape.Domain.Models.ApplicationUser
{
    public class GetApplicationUser:BaseApplicationUser
    {
        public string PhoneNumber { get; set; }

        public string Name { get; set; }
    }
}
