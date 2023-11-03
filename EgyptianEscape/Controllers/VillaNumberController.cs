using EgyptianEscape.Application.Repository.IRepository;
using EgyptianEscape.Domain.Entities;
using EgyptianEscape.Domain.Models.Villa;
using EgyptianEscape.Domain.Models.VillaNumber;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EgyptianEscape.Controllers
{
    public class VillaNumberController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public VillaNumberController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // GET: VillaController
        public ActionResult Index()
        {
            var villas = _unitOfWork.VillaNumberRepository.GetDetailsAll();
            return View(villas);
        }

        // GET: VillaController/Create
        //Getting the View

        public async Task<ActionResult> CreateAsync()
        {
            IEnumerable<SelectListItem> items = (await _unitOfWork.VillaRepository.GetAll<GetVilla>()).Select(u => new SelectListItem
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
        public async Task<ActionResult> CreateAsync(CreateVillaNumber villaNumber)
        {
            if (await _unitOfWork.VillaNumberRepository.Exists(villaNumber.Villa_Number))
            {
                TempData["Error"] = "Villa Number Already Exists";
                return RedirectToAction("Index");
            }

            var villas = _unitOfWork.VillaNumberRepository.GetDetails(villaNumber.VillaId);

            if (ModelState.IsValid)
            {
                await _unitOfWork.VillaNumberRepository.Add<CreateVillaNumber, VillaNumber>(villaNumber);
                TempData["Success"] = "Villa Number created successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        // GET: VillaController/Update/5
        public async Task<ActionResult> UpdateAsync(int Villa_NumberId)
        {

            IEnumerable<SelectListItem> items = (await _unitOfWork.VillaRepository.GetAll<GetVilla>()).Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            ViewBag.items = items;

            if (!await _unitOfWork.VillaNumberRepository.Exists(Villa_NumberId))
            {
                return RedirectToAction("Error", "Home");
            }
            var item = _unitOfWork.VillaNumberRepository.Get<UpdateVillaNumber>(Villa_NumberId);
            return View(item);
        }

        // POST: VillaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(UpdateVillaNumber villaNumber)
        {
            try
            {
                if (ModelState.IsValid && villaNumber.VillaId > 0)
                {
                    _unitOfWork.VillaNumberRepository.Update(villaNumber.Villa_Number, villaNumber);

                    TempData["Success"] = "The villaNumber was updated successfully";
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
        public async Task<ActionResult> DeleteAsync(int Villa_NumberId)
        {
            IEnumerable<SelectListItem> items = (await _unitOfWork.VillaRepository.GetAll<GetVilla>()).Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            ViewBag.items = items;

            if (!await _unitOfWork.VillaNumberRepository.Exists(Villa_NumberId))
            {
                return RedirectToAction("Error", "Home");
            }

            return View(_unitOfWork.VillaNumberRepository.Get<UpdateVillaNumber>(Villa_NumberId));
        }

        // POST: VillaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAsync(UpdateVillaNumber villaNumber)
        {
            var entity = await _unitOfWork.VillaNumberRepository.Get<UpdateVillaNumber>(villaNumber.Villa_Number);
            try
            {
                if (await _unitOfWork.VillaNumberRepository.Exists(entity.Villa_Number))
                {
                    await _unitOfWork.VillaNumberRepository.Delete(entity.Villa_Number);

                    TempData["Success"] = "The villaNumber was Deleted successfully";
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
