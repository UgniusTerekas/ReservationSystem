using ModelLayer.Dto.Gallery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interfaces
{
    public interface IGalleryServices
    {
        Task<GalleryDto> AddImageToGallery(CreateGalleryDto createGallery, int id);
    }
}
