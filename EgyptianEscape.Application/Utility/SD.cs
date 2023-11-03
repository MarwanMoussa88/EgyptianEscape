using EgyptianEscape.Domain.Entities;
using EgyptianEscape.Domain.Models.Booking;
using EgyptianEscape.Domain.Models.VillaNumber;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianEscape.Application.Utility
{
    public static class SD
    {
        public const string Role_Customer = "Customer";

        public const string Role_Admin = "Admin";

        public const string StatusPending = "Pending";

        public const string StatusApproved = "Approved";

        public const string StatusCheckIn = "Checked In";

        public const string StatusCompleted = "Completed";

        public const string StatusCancelled = "Cancelled";

        public const string StatusRefunded = "Refunded";


        public static int VillaRoomsAvaliableCount(int villaId,IEnumerable<GetVillaNumber> villaNumbers, DateTime checkInDate,int nights, IEnumerable<GetBooking> bookings)
        {
            List<int> bookingInDate = new();

            var roomsInVilla = villaNumbers.Where(x => x.Villa.Id == villaId).Count();

            int finalAvaliableRoomForAllNights = int.MaxValue;
            for(int i=0;i<nights;i++)
            {   

                var villasBooked = bookings.
                 Where(u => u.CheckInDate.Date <= checkInDate.AddDays(i)
                 && u.CheckOutDate.Date > checkInDate.AddDays(i)
                 && u.VillaId == villaId);

                foreach (var booking in villasBooked)
                {
                    if(!bookingInDate.Contains(booking.Id))
                    {
                        bookingInDate.Add(booking.Id);
                    }
                }

                var totalAvalaibleRooms = roomsInVilla - bookingInDate.Count;
                if(totalAvalaibleRooms == 0) 
                {
                    return 0;
                }
                else
                {
                    if(finalAvaliableRoomForAllNights>totalAvalaibleRooms)
                    {
                        finalAvaliableRoomForAllNights=totalAvalaibleRooms;
                    }
                }

                
            }

            return finalAvaliableRoomForAllNights;

        }
    }
}
