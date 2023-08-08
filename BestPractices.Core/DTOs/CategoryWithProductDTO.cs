using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestPractices.Core.DTOs
{
    public class CategoryWithProductDTO:CategoryDTO
    {
        public List<ProductDTO> Products { get; set; }
    }
}
