using BestPractices.Core.DTOs;
using BestPractices.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestPractices.Core.Services
{
    public interface ICategoryService:IService<Category>
    {
        Task<CustomResponseDTO<CategoryWithProductDTO>> GetSingleCategoryByIdWithProductAsync(int categoryId);
    }
}
