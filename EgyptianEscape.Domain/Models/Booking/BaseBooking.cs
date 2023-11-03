using EgyptianEscape.Domain.Models.Villa;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianEscape.Domain.Models.Booking
{
    public class BaseBooking
    {
        public int Id { get; set; }
        public GetVilla Villa { get; set; }

        public int VillaId { get; set; }
        public string UserId { get; set; }

        public string Status { get; set; }

        public DateTime BookingDate { get; set; }
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }

        public string? PhoneNumber { get; set; }
        [Required]
        public double TotalCost { get; set; }
        public int NumOfNights { get; set; }

        [Required]
        public DateTime CheckInDate { get; set; }
        [Required]
        public DateTime CheckOutDate { get; set; }


        public DateTime PaymentDate { get; set; }
        public string? StripeSessionId { get; set; }
        public string? StripePaymentId { get; set; }
        public DateTime ActualCheckIn { get; set; }
        public DateTime ActualCheckOut { get; set; }

        public bool IsPaymentSuccessful { get; set; } = false;


    }

}
