using EgyptianEscape.Domain.Models.Villa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianEscape.Application.ImageUploader
{
    public interface IImageUploader
    {
        void UploadImage(BaseVilla villa);
        void DeleteImage(BaseVilla villa);
    }
}
