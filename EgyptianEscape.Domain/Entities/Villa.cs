using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianEscape.Domain.Entities
{
    public class Villa
    {
        public int Id { get; set; }
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
        public string? ImageUrl { get; set; } ="https://placehold.co/600x400";

        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate
        {
            get; set;
        }
        [NotMapped]
        public IFormFile? Image { get; set; }
        [ValidateNever]
        public IEnumerable<EgyptianEscape.Domain.Entities.Amenity>? Amenities { get; set; }

        [NotMapped]
        public bool IsAvailable { get; set; } = true;

    }
}
