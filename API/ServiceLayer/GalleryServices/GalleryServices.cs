using DataLayer.Entities.Gallery;
using DataLayer.Interfaces;
using ModelLayer.Dto.Gallery;
using ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.GalleryServices
{

    public class GalleryServices : IGalleryServices
    {
        private readonly IGalleryRepository _galleryRepository;

        public GalleryServices(
            IGalleryRepository galleryRepository)
        {
            _galleryRepository = galleryRepository;
        }

        public async Task<GalleryDto> AddImageToGallery(CreateGalleryDto createGallery, int id)
        {
            var galleryEntity = new GalleryEntity
            {
                ImageName = createGallery.Name,
                ImageLocation = createGallery.Location,
                EntertainmentId = id
            };

            var image = await _galleryRepository.CreateGallery(galleryEntity);

            return new GalleryDto
            {
                ImageId = image.ImageId,
                ImageName = image.ImageName,
                ImageLocation = image.ImageLocation,
            };
        }
    }
}
