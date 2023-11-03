using EgyptianEscape.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianEscape.Domain.Models.VillaNumber
{
    public class BaseVillaNumber
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Villa Number")]
        public int Villa_Number { get; set; }

        [ForeignKey(nameof(VillaId))]
        [ValidateNever]

        public Entities.Villa? Villa { get; set; }
        [Display(Name = "Villa Id")]
        public int VillaId { get; set; }

        public string? SpecialDetails { get; set; }

    }
}
