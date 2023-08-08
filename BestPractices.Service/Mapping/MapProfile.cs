using AutoMapper;
using BestPractices.Core.DTOs;
using BestPractices.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestPractices.Service.Mapping
{
    public class MapProfile:Profile
    {
        public MapProfile()
        {
            CreateMap<Product,ProductDTO>().ReverseMap();  //ReverseMap ile her ikisinde de çevirebiliriz.
            //Yani response a da repusete de dönmesı ıcın entıty ıle dto bırıbırne donusebılır.
            CreateMap<Category,CategoryDTO>().ReverseMap();
            CreateMap<ProductFeature,ProductFeatureDTO>().ReverseMap();
            CreateMap<ProductUpdateDTO,Product>(); //Bunun tersine ihtiyacımız yok.
            CreateMap<Product, ProductWithCategoryDTO>();
            CreateMap<Category, CategoryWithProductDTO>();

        }
    }
}
