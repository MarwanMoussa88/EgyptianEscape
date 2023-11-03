using EgyptianEscape.Domain.Models.Villa;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianEscape.Domain.Entities
{
    public class Booking
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; }
        public string UserId { get; set; }

        [ForeignKey(nameof(VillaId))]

        public Villa Villa { get; set; }
        [Required]
        public int VillaId { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }

        public string? PhoneNumber { get; set; }
        [Required]
        public double TotalCost { get; set; }
        public int NumOfNights { get; set; }
        public string? Status { get; set; }

        [Required]
        public DateTime BookingDate { get; set; }
        [Required]
        public DateTime CheckInDate { get; set; }
        [Required]
        public DateTime CheckOutDate { get; set; }

        public bool IsPaymentSuccessful { get; set; } = false;
        public DateTime PaymentDate { get; set; }
        public string? StripeSessionId { get; set; }
        public string? StripePaymentId { get; set; }
        public DateTime ActualCheckIn { get; set; }
        public DateTime ActualCheckOut { get; set; }

        public int VillaNumber { get; set; }

    }
}
