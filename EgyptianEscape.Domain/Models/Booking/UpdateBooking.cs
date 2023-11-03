using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianEscape.Domain.Models.Booking
{
    public class UpdateBooking:BaseBooking
    {
        [Key]
        public int Id { get; set; }

    }
}
