using EgyptianEscape.Domain.Models.Amenity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EgyptianEscape.Domain.Models.Villa
{
    public class BaseVilla
    {
        [Required]
        [MaxLength(50)]
        public required string Name { get; set; }
        public string? Description { get; set; }
        [Display(Name = "Price per night")]
        [Range(10, 10000)]

        public double Price { get; set; }

        public int Area { get; set; }
        [Range(1, 10)]
        public int Occupancy { get; set; }
        [Display(Name = "Image Url")]
        public string? ImageUrl { get; set; }

        [NotMapped]
        public IFormFile? Image { get; set; }
        [ValidateNever]
        public ICollection<GetAmenity>? Amenities { get; set; }
        [NotMapped]
        public bool IsAvailable { get; set; } = true;
    }
}
