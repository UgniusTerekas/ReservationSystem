using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Dto.Category;
using ModelLayer.Dto.Gallery;
using ServiceLayer.GalleryServices;
using ServiceLayer.Interfaces;
using static System.Net.Mime.MediaTypeNames;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GalleryController : ControllerBase
    {
        private readonly IGalleryServices _galleryServices;

        public GalleryController(IGalleryServices galleryServices)
        {
            _galleryServices = galleryServices;
        }

        [HttpPost("uploadImage")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UploadImage([FromForm] UploadGalleryDto uploadGalleryDto)
        {
            if (uploadGalleryDto.FileNames.Count == 0 
                || uploadGalleryDto.Images.Count == 0)
            {
                return BadRequest("No Images where to send it");
            }

            var location = Directory.GetCurrentDirectory() + "\\Images";

            try
            {
                var count = 0;
                foreach (var image in uploadGalleryDto.Images)
                {
                    var locationWithName = location + "/" + uploadGalleryDto.FileNames[count];
                    using (Stream stream = new FileStream(locationWithName, FileMode.Create))
                    {
                        image.CopyTo(stream);
                    }
                    count++;

                    var galleryDto = new GalleryDto
                    {
                        EntertainmentId = uploadGalleryDto.Id,
                        ImageLocation = "/Images/" + image.FileName,
                        ImageName = image.FileName,
                    };
                    
                    _galleryServices.AddImageToDataBase(galleryDto);
                }
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }
    }
}
