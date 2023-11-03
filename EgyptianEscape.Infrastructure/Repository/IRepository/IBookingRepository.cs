using EgyptianEscape.Application.Repository.IRepository;
using EgyptianEscape.Domain.Entities;
using EgyptianEscape.Domain.Models.Booking;
using EgyptianEscape.Domain.Models.VillaNumber;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianEscape.Infrastructure.Repository.IRepository
{
    public interface IBookingRepository:IGenericRepository<Booking>
    {

        Task<IEnumerable<GetBooking>> GetDetails(string userId);
        IEnumerable<GetBooking> GetDetailsAll();

        Task UpdateStatus(int bookingId,string bookingStatus);
        Task UpdateStridePayment(int bookingId,string sessionId,string paymentId);
    }
}
