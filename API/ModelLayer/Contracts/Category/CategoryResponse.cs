using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.Contracts.Category
{
    public class CategoryResponse
    {
        public string CategoryName { get; set; }

        public byte[] CategoryImage { get; set; }
    }
}
