using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianEscape.Domain.Models.Amenity
{
    public class BaseAmenity
    {
        public string Name { get; set; }
        public string? Description { get; set; }

        [ForeignKey(nameof(VillaId))]
        [ValidateNever]
        public Entities.Villa Villa { get; set; }
        public int VillaId { get; set; }

    }
}
