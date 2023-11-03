using AutoMapper;
using AutoMapper.QueryableExtensions;
using EgyptianEscape.Application.Repository;
using EgyptianEscape.Application.Utility;
using EgyptianEscape.Domain.Data;
using EgyptianEscape.Domain.Entities;
using EgyptianEscape.Domain.Models.Booking;
using EgyptianEscape.Domain.Models.VillaNumber;
using EgyptianEscape.Infrastructure.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace EgyptianEscape.Infrastructure.Repository
{
    public class BookingRepository : GenericRepository<Booking>, IBookingRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public BookingRepository(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        public async Task<IEnumerable<GetBooking>> GetDetails(string userId)
        {
            var bookings = _context.Set<Booking>()
                 .Include(p => p.Villa)
                 .Where(p => p.UserId==userId)
                 .ProjectTo<GetBooking>(_mapper.ConfigurationProvider)
                 .ToList();
            return bookings;
        }

        public IEnumerable<GetBooking> GetDetailsAll()
        {
            var bookings = _context.Set<Booking>()
                         .Include(p => p.Villa)
                         .ProjectTo<GetBooking>(_mapper.ConfigurationProvider)
                         .ToList();


            return bookings;

        }

        public async Task UpdateStatus(int bookingId, string bookingStatus)
        {
           var bookingFromDb= await _context.Set<Booking>().FirstOrDefaultAsync(m=>m.Id==bookingId);
            if( bookingFromDb !=null)
            {
               bookingFromDb.Status=bookingStatus;
               if(bookingStatus==SD.StatusCheckIn)
                    bookingFromDb.ActualCheckIn = DateTime.Now;
               if(bookingStatus == SD.StatusCompleted)
                    bookingFromDb.ActualCheckOut = DateTime.Now;

                bookingFromDb.Status = bookingStatus;
                _context.Update(bookingFromDb);
                await _context.SaveChangesAsync();
            }



        }

        public async Task UpdateStridePayment(int bookingId, string sessionId, string paymentId)
        {

            var bookingFromDb = await _context.Set<Booking>().FirstOrDefaultAsync(m => m.Id == bookingId);
            
            if (bookingFromDb != null)
            {
                if(!string.IsNullOrEmpty(sessionId)) 
                {
                    bookingFromDb.StripePaymentId = paymentId;
                    bookingFromDb.PaymentDate = DateTime.Now;
                    bookingFromDb.StripeSessionId = sessionId;
                    bookingFromDb.IsPaymentSuccessful = true;
                    _context.Update(bookingFromDb);
                    await _context.SaveChangesAsync();
                }

            }

        }
    }
}
