using EgyptianEscape.Application.ImageUploader;
using EgyptianEscape.Application.Repository.IRepository;
using EgyptianEscape.Domain.Data;
using EgyptianEscape.Domain.Entities;
using EgyptianEscape.Domain.Models.Villa;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace EgyptianEscape.Controllers
{
    [Authorize]
    public class VillaController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IImageUploader _imageUploader;

        public VillaController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment, IImageUploader imageUploader)
        {
            _unitOfWork = unitOfWork;
            this._webHostEnvironment = webHostEnvironment;
            this._imageUploader = imageUploader;
        }
        // GET: VillaController
        public async Task<IActionResult> Index()
        {
            var villas =await _unitOfWork.VillaRepository.GetAll<GetVilla>();
            return View(villas);
        }

        // GET: VillaController/Create
        //Getting the View
        public ActionResult Create()
        {
            return View();
        }

        // POST: VillaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateVilla villa)
        {

            try
            {
                if (villa.Name == villa.Description)
                {
                    ModelState.AddModelError("", "Description can't be the same as the name");
                }
                if (ModelState.IsValid)
                {

                    _imageUploader.UploadImage(villa);

                    _unitOfWork.VillaRepository.Add<CreateVilla, Villa>(villa);

                    TempData["Success"] = "The villa was created successfully";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: VillaController/Update/5
        public async Task<ActionResult> UpdateAsync(int villaId)
        {
            if (!await _unitOfWork.VillaRepository.Exists(villaId))
            {
                return RedirectToAction("Error", "Home");
            }

            return View(await _unitOfWork.VillaRepository.Get<UpdateVilla>(villaId));
        }

        // POST: VillaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(UpdateVilla villa)
        {

            try
            {
                if (ModelState.IsValid && villa.Id > 0)
                {
                    _imageUploader.UploadImage(villa);
                    _unitOfWork.VillaRepository.Update(villa.Id, villa);

                    TempData["Success"] = "The villa was updated successfully";
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
        public async Task<IActionResult> Delete(int villaId)
        {
            if (!await _unitOfWork.VillaRepository.Exists(villaId))
            {
                return RedirectToAction("Error", "Home");
            }

            return View(await _unitOfWork.VillaRepository.Get<UpdateVilla>(villaId));
        }

        // POST: VillaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAsync(UpdateVilla villa)
        {
            villa = await _unitOfWork.VillaRepository.Get<UpdateVilla>(villa.Id);
            try
            {
                if (villa.Id > 0)
                {
                    _imageUploader.DeleteImage(villa);
                    await _unitOfWork.VillaRepository.Delete(villa.Id);

                    TempData["Success"] = "The villa was delete successfully";
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
