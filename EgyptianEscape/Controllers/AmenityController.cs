using EgyptianEscape.Application.Repository.IRepository;
using EgyptianEscape.Application.Utility;
using EgyptianEscape.Domain.Entities;
using EgyptianEscape.Domain.Models.Amenity;
using EgyptianEscape.Domain.Models.Villa;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EgyptianEscape.Controllers
{
    [Authorize(Roles =SD.Role_Admin)]
    public class AmenityController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public AmenityController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // GET: VillaController
        public ActionResult Index()
        {
            var villas = _unitOfWork.AmenityRepository.GetDetailsAll();
            return View(villas);
        }

        // GET: VillaController/Create
        //Getting the View

        public async Task<ActionResult> CreateAsync()
        {
            IEnumerable<SelectListItem> items =  (await _unitOfWork.VillaRepository.GetAll<GetVilla>()).Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            ViewBag.items = items;

            return View();
        }

        // POST: VillaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateAmenity amenity)
        {
            
            if (ModelState.IsValid)
            {
                _unitOfWork.AmenityRepository.Add<CreateAmenity, Amenity>(amenity);
                TempData["Success"] = "An Amenity was created successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        // GET: VillaController/Update/5
        public async Task<ActionResult> UpdateAsync(int AmenityId)
        {

            IEnumerable<SelectListItem> items = (await _unitOfWork.VillaRepository.GetAll<GetVilla>()).Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            ViewBag.items = items;

            if (!await _unitOfWork.AmenityRepository.Exists(AmenityId))
            {
                return RedirectToAction("Error", "Home");
            }
            var item = _unitOfWork.AmenityRepository.Get<UpdateAmenity>(AmenityId);
            return View(item);
        }

        // POST: VillaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(UpdateAmenity updateAmenity)
        {
            try
            {
                if (ModelState.IsValid && updateAmenity.Id > 0)
                {
                    _unitOfWork.AmenityRepository.Update(updateAmenity.Id, updateAmenity);

                    TempData["Success"] = "The Amenity was updated successfully";
                    return RedirectToAction(nameof(Index));
                }
                return RedirectToAction("Error", "Home");
            }
            catch
            {
                return View();
            }

        }

        // GET: VillaController/Delete/5
        public async Task<ActionResult> DeleteAsync(int AmenityId)
        {
            IEnumerable<SelectListItem> items = (await _unitOfWork.VillaRepository.GetAll<GetVilla>()).Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            ViewBag.items = items;

            if (!await _unitOfWork.AmenityRepository.Exists(AmenityId))
            {
                return RedirectToAction("Error", "Home");
            }

            return View(_unitOfWork.AmenityRepository.Get<UpdateAmenity>(AmenityId));
        }

        // POST: VillaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAsync(UpdateAmenity updateAmenity)
        {
            var entity = _unitOfWork.AmenityRepository.Get<UpdateAmenity>(updateAmenity.Id);
            try
            {
                if (await _unitOfWork.AmenityRepository.Exists(updateAmenity.Id))
                {
                    await _unitOfWork.AmenityRepository.Delete(updateAmenity.Id);

                    TempData["Success"] = "The Amenity was Deleted successfully";
                    return RedirectToAction(nameof(Index));
                }
                return RedirectToAction("Error", "Home");
            }
            catch
            {
                return View();
            }
        }
    }
}
