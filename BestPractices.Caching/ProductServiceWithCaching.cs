using AutoMapper;
using BestPractices.Core.DTOs;
using BestPractices.Core.Entities;
using BestPractices.Core.Repositories;
using BestPractices.Core.Services;
using BestPractices.Core.UnitOfWorks;
using BestPractices.Service.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BestPractices.Caching
{
    public class ProductServiceWithCaching : IProductService   //proxy design patern'e benziyor
    {

        private const string CacheProductKey = "productsCache";
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;
        private readonly IProductRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public ProductServiceWithCaching(IMapper mapper, IMemoryCache memoryCache, IProductRepository repository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _memoryCache = memoryCache;
            _repository = repository;
            _unitOfWork = unitOfWork;

            if(!_memoryCache.TryGetValue(CacheProductKey, out _)) //alttre ile memoryde yer tutmasını engellıyoruz
                //uygulama ılk ayaga kalktıgınde cach yoksa oluşturacak
            {
                _memoryCache.Set(CacheProductKey, _repository.GetProductWithCategory().Result); //RESULT OLMASININ SEBEİ ASENKRON CAGIRAMIYORUIZ SENKRON OLMASI LAZIM
            }
        }


        public async Task<Product> addAsync(Product entity)
        {
            await _repository.addAsync(entity); //repoya ekledı
            await _unitOfWork.CommitAsync();   //savecganges dedi
            await CacheAllProductsAsync();     //cache kaydetti
            return entity;                    //dondu
        }

        public async Task<IEnumerable<Product>> AddRangeAsync(IEnumerable<Product> Entities)
        {
            await _repository.AddRangeAsync(Entities); //repoya ekledı
            await _unitOfWork.CommitAsync();            //savecganges dedi
            await CacheAllProductsAsync();               //cache kaydetti
            return Entities;                          //dondu
        }

        public Task<bool> AnyAsync(Expression<Func<Product, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetAllAsync()
        {
            return Task.FromResult(_memoryCache.Get<IEnumerable<Product>>(CacheProductKey));
        }

        public Task<Product> GetByIdAsync(int id)
        {

            var product= _memoryCache.Get<List<Product>>(CacheProductKey).FirstOrDefault(x => x.Id == id);

            if (product == null)
            {
                throw new NotFoundException($"{typeof(Product).Name} {id} not found");
            }

            return Task.FromResult(product);

        }

        public Task<CustomResponseDTO<List<ProductWithCategoryDTO>>> GetProductWithCategory() {  
            var products = _memoryCache.Get<IEnumerable<Product>>(CacheProductKey);
            var productsWithCateogryDto = _mapper.Map<List<ProductWithCategoryDTO>>(products);
            return Task.FromResult(CustomResponseDTO<List<ProductWithCategoryDTO>>.Success(200, productsWithCateogryDto));  
        }

        public async Task RemoveAsync(Product entity)
        {
            _repository.Remove(entity);     //sildi
            await _unitOfWork.CommitAsync(); //sacechanges dedi
            await CacheAllProductsAsync();   //cach'e kaydetti
        }

        public async Task RemoveRangeAsync(IEnumerable<Product> entities)
        {
            _repository.RemoveRange(entities);
            await _unitOfWork.CommitAsync();
            await CacheAllProductsAsync();
        }

        public async Task UpdateAsync(Product entity)
        {
            _repository.Update(entity);
            await _unitOfWork.CommitAsync();
            await CacheAllProductsAsync();
        }

        public IQueryable<Product> Where(Expression<Func<Product, bool>> expression)
        {
            return _memoryCache.Get<List<Product>>(CacheProductKey).Where(expression.Compile()).AsQueryable();
        }

        public async Task CacheAllProductsAsync() //her cagırıldıgında 0dan datayı cekıp cahlıyor.
        {
            _memoryCache.Set(CacheProductKey, _repository.GetProductWithCategory().Result);
        }
    }
}
//ASync Asenkron