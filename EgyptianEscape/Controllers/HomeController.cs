
using EgyptianEscape.Application.Repository.IRepository;
using EgyptianEscape.Application.Utility;
using EgyptianEscape.Domain.Models.Villa;
using EgyptianEscape.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EgyptianEscape.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {

            this._unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            HomeVM vm = new HomeVM
            {
                Villas = _unitOfWork.VillaRepository.GetDetailsAll(),
                Nights = 1,
                CheckInDate = DateTime.Now
            };
            return View(vm);
        }
        [HttpPost]

        public async Task<IActionResult> GetVillaByDateAsync(int nights, DateTime checkInDate)
        {

            var villas = _unitOfWork.VillaRepository.GetDetailsAll();
            var villaNumber = _unitOfWork.VillaNumberRepository.GetDetailsAll();
            var bookings = _unitOfWork.BookingRepository.GetDetailsAll().
                Where(u => u.Status == SD.StatusApproved || u.Status == SD.StatusCheckIn).ToList();

            foreach (var villa in villas)
            {
                int roomsAvaliable = SD.VillaRoomsAvaliableCount(villa.Id, villaNumber, checkInDate, nights, bookings);
                villa.IsAvailable = roomsAvaliable > 0 ? true : false;
            }
            HomeVM homeVM = new HomeVM { Villas = villas, CheckInDate = checkInDate, Nights = nights };
            return PartialView("_VillaList", homeVM);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}