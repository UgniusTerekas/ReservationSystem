using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.Dto.Gallery
{
    public class UploadGalleryDto
    {
        public int Id { get; set; }

        public List<string> FileNames { get; set; }

        public List<IFormFile>? Images { get; set; }
    }
}
