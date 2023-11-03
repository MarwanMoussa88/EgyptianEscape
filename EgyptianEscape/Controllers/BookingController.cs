using EgyptianEscape.Application.Repository.IRepository;
using EgyptianEscape.Application.Utility;
using EgyptianEscape.Domain.Entities;
using EgyptianEscape.Domain.Models.ApplicationUser;
using EgyptianEscape.Domain.Models.Booking;
using EgyptianEscape.Domain.Models.Villa;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;
using System.Net;
using System.Security.Claims;

namespace EgyptianEscape.Controllers
{
    public class BookingController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public BookingController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {

            return View(await _unitOfWork.BookingRepository.GetAll<GetBooking>());
        }


        [Authorize]
        public async Task<IActionResult> FinalizeBookingAsync(int villaId, DateTime checkInDate, int nights)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            GetApplicationUser user = await _unitOfWork.UserRepository.Get<GetApplicationUser, string>(userId);


            CreateBooking booking = new CreateBooking
            {
                NumOfNights = nights,
                VillaId = villaId,
                CheckInDate = checkInDate,
                CheckOutDate = checkInDate.AddDays(nights),
                Villa = _unitOfWork.VillaRepository.GetDetails(villaId),
                UserId = userId,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Name = user.Name

            };
            booking.TotalCost = booking.Villa.Price * nights;

            return View(booking);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> FinalizeBooking(CreateBooking booking)
        {
            var getVilla = await _unitOfWork.VillaRepository.Get<GetVilla>(booking.VillaId);
            booking.TotalCost = getVilla.Price * booking.NumOfNights;
            booking.Status = SD.StatusPending;
            booking.BookingDate = DateTime.Now;



            var villas = _unitOfWork.VillaRepository.GetDetailsAll();
            var villaNumber = _unitOfWork.VillaNumberRepository.GetDetailsAll();
            var bookings = _unitOfWork.BookingRepository.GetDetailsAll().
                Where(u => u.Status == SD.StatusApproved || u.Status == SD.StatusCheckIn).ToList();

            int roomsAvaliable = SD.VillaRoomsAvaliableCount(getVilla.Id, villaNumber, booking.CheckInDate, booking.NumOfNights, bookings);

            if (roomsAvaliable == 0)
            {
                TempData["Error"] = "Room has been sold out";
                return RedirectToAction(nameof(FinalizeBooking),
                    new
                    {
                        villaId = booking.VillaId,
                        checkInDate = booking.CheckInDate,
                        nights = booking.NumOfNights
                    });
            }



            var book = await _unitOfWork.BookingRepository.Add<CreateBooking, Booking>(booking);

            var domain = Request.Scheme + "://" + Request.Host.Value + "/";

            var options = new SessionCreateOptions
            {
                LineItems = new List<SessionLineItemOptions>(),
                Mode = "payment",
                SuccessUrl = domain + $"booking/BookingConfirmation?bookingId={book.Id}",
                CancelUrl = domain + $"booking/FinalizeBooking?villaId={book.VillaId}&checkInDate={book.CheckInDate}&nights={book.NumOfNights}",
            };
            options.LineItems.Add(new SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    UnitAmount = (long)book.TotalCost * 100,
                    Currency = "usd",
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = book.Villa.Name,
                        //Images= new List<string> { domain + book.Villa.ImageUrl},
                    }
                },
                Quantity = 1,
            });

            var service = new SessionService();
            Session session = service.Create(options);
            await _unitOfWork.BookingRepository.UpdateStridePayment(book.Id, session.Id, session.PaymentIntentId);
            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);
        }

        [Authorize]
        public async Task<IActionResult> BookingConfirmationAsync(int bookingId)
        {
            Booking booking = await _unitOfWork.BookingRepository.Get<Booking>(bookingId);
            var service = new SessionService();
            var session = service.Get(booking.StripeSessionId);

            if (booking.Status == SD.StatusPending)
            {
                if (session.PaymentStatus == "paid")
                {
                    booking.StripePaymentId = session.PaymentIntentId;
                    await _unitOfWork.BookingRepository.UpdateStatus(bookingId, SD.StatusApproved);
                    await _unitOfWork.BookingRepository.Update(bookingId, booking);
                }
            }


            return View(bookingId);
        }

        public async Task<IActionResult> BookingDetails(int bookingId)
        {
            var booking = await _unitOfWork.BookingRepository.Get<GetBooking>(bookingId);
            booking.Villa = await _unitOfWork.VillaRepository.Get<GetVilla>(booking.VillaId);
            return View(booking);
        }

        [HttpPost]
        [Authorize(Roles = SD.Role_Admin)]
        public async Task<IActionResult> CheckIn(GetBooking booking)
        {
            await _unitOfWork.BookingRepository.UpdateStatus(booking.Id, SD.StatusCheckIn);
            TempData["Success"] = "Booking Updated Successfully";
            return RedirectToAction(nameof(BookingDetails), new { bookingId = booking.Id });
        }
        [HttpPost]
        [Authorize(Roles = SD.Role_Admin)]
        public async Task<IActionResult> CheckOut(GetBooking booking)
        {
            await _unitOfWork.BookingRepository.UpdateStatus(booking.Id, SD.StatusCompleted);
            TempData["Success"] = "Booking Updated Successfully";
            return RedirectToAction(nameof(BookingDetails), new { bookingId = booking.Id });
        }
        public async Task<IActionResult> CheckCancelled(GetBooking booking)
        {
            await _unitOfWork.BookingRepository.UpdateStatus(booking.Id, SD.StatusCancelled);
            TempData["Success"] = "Booking Updated Successfully";
            return RedirectToAction(nameof(BookingDetails), new { bookingId = booking.Id });
        }


        #region API Calls
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll(string status)
        {
            IEnumerable<GetBooking> bookings;

            if (User.IsInRole(SD.Role_Admin))
            {
                bookings = await _unitOfWork.BookingRepository.GetAll<GetBooking>();
            }
            else
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
                bookings = await _unitOfWork.BookingRepository.GetDetails(userId);
            }


            bookings = bookings.Where(u => u.Status.Equals(status));

            return Json(new { data = bookings });

        }

        #endregion


    }
}
