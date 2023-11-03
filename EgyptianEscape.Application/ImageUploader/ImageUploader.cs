using EgyptianEscape.Domain.Entities;
using EgyptianEscape.Domain.Models.Villa;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EgyptianEscape.Application.ImageUploader
{
    public class ImageUploader : IImageUploader
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ImageUploader(IWebHostEnvironment webHostEnvironment)
        {
            this._webHostEnvironment = webHostEnvironment;
        }

        public void DeleteImage(BaseVilla villa)
        {

            var oldImage = Path.Combine(_webHostEnvironment.WebRootPath, villa.ImageUrl.TrimStart('\\'));
            if (File.Exists(oldImage))
            {
                File.Delete(oldImage);
            }
        }

        void IImageUploader.UploadImage(BaseVilla villa)
        {
            if (villa.Image != null)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(villa.Image.FileName);
                string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, @"images\VillaImages");

                if (!string.IsNullOrEmpty(villa.ImageUrl))
                {
                    var oldImage = Path.Combine(_webHostEnvironment.WebRootPath, villa.ImageUrl.TrimStart('\\'));
                    if (File.Exists(oldImage))
                    {
                        File.Delete(oldImage);
                    }
                }

                using (var filestream = new FileStream(Path.Combine(imagePath, fileName), FileMode.Create))
                {
                    villa.Image.CopyTo(filestream);
                    villa.ImageUrl = @"\Images\VillaImages\" + fileName;
                }
            }

        }


    }
}
