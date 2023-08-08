using BestPractices.Core.Entities;
using BestPractices.Core.Repositories;
using BEstPractices.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestPractices.Repository.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository //burada direkt Iproduct'u alsaydık
        //genericrepostıyry de zaten yazdıgımız sınıfların methodlarının govdelerını bır daha yamzmaız gerekırdı
        //bunun sebebi ise Iproductrepository'nin IGenericRepository'den kalıtım alması.
    {
        public ProductRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<Product>> GetProductWithCategory()
        {
            //Eager Loading
            return await _context.Products.Include(x=>x.Category).ToListAsync(); //datayı cekerken kategorılerın de alınmasını ıstedık. (lazy loading de var araştırılabilir.)
        }
    }
}
