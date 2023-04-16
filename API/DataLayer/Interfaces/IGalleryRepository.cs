using DataLayer.Entities.Gallery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Interfaces
{
    public interface IGalleryRepository
    {
        Task<GalleryEntity> CreateGallery(GalleryEntity gallery);
        Task DeleteGallery(GalleryEntity gallery);
        Task<List<GalleryEntity>> GetGalleries();
        Task<GalleryEntity> GetOneGallery(int galleryId);
        Task<GalleryEntity> UpdateGallery(GalleryEntity gallery);
        Task<bool> AddImageToDataBase(GalleryEntity galleryEntity);
    }
}
