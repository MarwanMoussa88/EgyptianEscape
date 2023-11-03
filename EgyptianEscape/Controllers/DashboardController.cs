using EgyptianEscape.Application.Repository.IRepository;
using EgyptianEscape.Application.Utility;
using EgyptianEscape.Domain.Entities;
using EgyptianEscape.Domain.Models.ApplicationUser;
using EgyptianEscape.Domain.Models.Booking;
using EgyptianEscape.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EgyptianEscape.Controllers
{
    [Authorize(Roles = SD.Role_Admin)]
    public class DashboardController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        readonly DateTime previousMonthStartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month - 1, 1);
        readonly DateTime currentMonthStartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        static int previousMonth = DateTime.Now.Month == 1 ? 12 : DateTime.Now.Month - 1;

        public DashboardController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {

            return View();
        }

        public async Task<IActionResult> GetTotalBookingRadialChartData()
        {
            var totalBookings = (await _unitOfWork.BookingRepository.GetAll<GetBooking>())
                .Where(u => u.Status != SD.StatusPending || u.Status == SD.StatusCancelled);

            double countByCurrentMonth = totalBookings.Where(u => u.BookingDate >= currentMonthStartDate && u.BookingDate <= DateTime.Now)
                .Count();

            double countByPreviousMonth = totalBookings.Where(u => u.BookingDate >= previousMonthStartDate && u.BookingDate <= currentMonthStartDate)
                .Count();




            return Json(GetRadialChartDataModel(totalBookings.Count(), countByCurrentMonth, countByPreviousMonth));



        }
        public async Task<IActionResult> GetRegisteredUserChartData()
        {
            var totalUsers = await _unitOfWork.UserRepository.GetAll<GetApplicationUser>();


            double countByCurrentMonth = totalUsers.Where(u => u.CreatedAt >= currentMonthStartDate && u.CreatedAt <= DateTime.Now)
                .Count();

            double countByPreviousMonth = totalUsers.Where(u => u.CreatedAt >= previousMonthStartDate && u.CreatedAt <= currentMonthStartDate)
                .Count();




            return Json(GetRadialChartDataModel(totalUsers.Count(), countByCurrentMonth, countByPreviousMonth));



        }

        public async Task<IActionResult> GetRevenueChartData()
        {
            var totalBookings = (await _unitOfWork.BookingRepository.GetAll<GetBooking>())
                .Where(u => u.Status != SD.StatusPending || u.Status == SD.StatusCancelled);

            var totalRevenue = Convert.ToInt32(totalBookings.Sum(u => u.TotalCost));

            double countByCurrentMonth = totalBookings.Where(u => u.BookingDate >= currentMonthStartDate && u.BookingDate <= DateTime.Now)
                .Sum(u => u.TotalCost);

            double countByPreviousMonth = totalBookings.Where(u => u.BookingDate >= previousMonthStartDate && u.BookingDate <= currentMonthStartDate)
                .Sum(u => u.TotalCost);




            return Json(GetRadialChartDataModel(totalRevenue, countByCurrentMonth, countByPreviousMonth));



        }

        public async Task<IActionResult> GetBookingPieChartData()
        {
            var totalBookings = (await _unitOfWork.BookingRepository.GetAll<GetBooking>())
                .Where(
                (u => u.Status != SD.StatusPending ||(u.Status == SD.StatusCancelled)  &&
                (u.BookingDate >= DateTime.Now.AddDays(-30))));

            var customerWithOneBooking = totalBookings.GroupBy(b => b.UserId).Where(x => x.Count() == 1).Select(x=>x.Key).ToList();

            int bookingsByNewCustomer= customerWithOneBooking.Count();
            int bookingsbyReturningCustomer = totalBookings.Count() - bookingsByNewCustomer;
            PieChartVM vm = new PieChartVM
            {
                Labels = new string[] { "New Customer Booking", "Returning Customer Booking" },
                Series = new decimal[] { bookingsByNewCustomer, bookingsbyReturningCustomer }
            };

                
            return Json(vm);



        }

        public async Task<IActionResult> GetMemberAndBookingChartData()
        {
            var bookingData = _unitOfWork.BookingRepository.GetDetailsAll()
                .Where(u => u.BookingDate >= DateTime.Now.AddDays(-30) && u.BookingDate <= DateTime.Now)
                .GroupBy(b => b.BookingDate.Date)
                .Select(u => new { 
                    DateTime = u.Key,
                    NewBookingCount=u.Count()});

            var userData = (await _unitOfWork.UserRepository.GetAll<GetApplicationUser>())
              .Where(u => u.CreatedAt >= DateTime.Now.AddDays(-30) && u.CreatedAt <= DateTime.Now)
              .GroupBy(b => b.CreatedAt.Date)
              .Select(u => new {
                  DateTime = u.Key,
                  NewCustomerCount = u.Count()
              });

            var leftJoin = bookingData.GroupJoin(userData, booking => booking.DateTime, user => user.DateTime,
                (booking, user) => new
                {
                    booking.DateTime,
                    booking.NewBookingCount,
                    NewCustomerCount=user.Select(x=>x.NewCustomerCount).FirstOrDefault()
       
                 });

            var rightJoin = userData.GroupJoin(bookingData, user => user.DateTime, user => user.DateTime,
                (user, booking) => new
                {
                    user.DateTime,
                    NewBookingCount=booking.Select(x=>x.NewBookingCount).FirstOrDefault(),
                    user.NewCustomerCount

                });

            var mergedDate=leftJoin.Union(rightJoin).OrderBy(x=>x.DateTime).ToList();

            var newBookingData = mergedDate.Select(x => x.NewBookingCount).ToArray();
            var newUserData = mergedDate.Select(x=>x.NewCustomerCount).ToArray();
            var categories = mergedDate.Select(x => x.DateTime.ToString("dd/MM/yyyy")).ToArray();

            List<ChartData> chartData = new List<ChartData>
            {
                new ChartData
                {
                    Name="New Bookings",
                    Data=newBookingData,
                },
                new ChartData
                {
                    Name="New Users",
                    Data=newUserData,
                },
            };

            LineChartVM lineChartVM = new LineChartVM
            {
                Categories=categories,
                Series=chartData
            };
           
            return Json(lineChartVM);
        }

        private static RadialBarChartVM GetRadialChartDataModel(int totalCount, double currentMonthCount, double previousMonthCount)
        {
            int increaseDecreaseRatio = 100;
            if (previousMonthCount != 0)
            {
                double total = (currentMonthCount - previousMonthCount) / previousMonthCount * 100;
                increaseDecreaseRatio = Convert.ToInt32(total);
            }

            return new RadialBarChartVM
            {
                TotalCount = totalCount,
                CountInCurrentMonth = Convert.ToInt32(currentMonthCount),
                HasRatioIncreased = currentMonthCount > previousMonthCount,
                Series = new int[] { increaseDecreaseRatio }

            };
        }



    }
}
