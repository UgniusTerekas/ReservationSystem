using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ModelLayer.Contracts.Category
{
    public class CategoryRequest
    {
        public string CategoryName { get; set; }

        public IFormFile CategoryImage { get; set; }
    }
}
