using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianEscape.Domain.Models.Villa
{
    public class UpdateVilla : BaseVilla
    {
        [Required]
        public int Id { get; set; }

    }
}
