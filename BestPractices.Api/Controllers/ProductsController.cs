using AutoMapper;
using BestPractices.Api.Filters;
using BestPractices.Api.Validations;
using BestPractices.Core.DTOs;
using BestPractices.Core.Entities;
using BestPractices.Core.Repositories;
using BestPractices.Core.Services;
using BestPractices.Repository.Repositories;
using BestPractices.Service.Services;
using BEstPractices.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BestPractices.Api.Controllers
{
    //[ValidateFilterAttribute] //Bu kısmı program cs içerisinde yapıyoruz
    public class ProductsController : CustomBaseController
    {
        private readonly IMapper _mapper;
        private readonly IProductService _service;
        AppDbContext _context;
        public ProductsController(IMapper mapper, AppDbContext context, IProductService productService)
        {
            _context = context;
            _mapper = mapper;
            _service = productService;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var products= await _service.GetAllAsync();
            var productsDtos = _mapper.Map<List<ProductDTO>>(products.ToList());
            //return Ok(CustomResponseDTO<List<ProductDTO>>.Success(200,productsDtos)); //burayı CustoöbaseControllerde yapıyoruz.
            return CreateActionResult(CustomResponseDTO<List<ProductDTO>>.Success(200, productsDtos));
        }


        [ServiceFilter(typeof(NotFoundFilter<Product>))] //burada id olmadıgı zaman hiç medhodun içerisine girmşyor
        //bunu istersek bu şekilde istersek service katmanındakı kendı yazdıgımız ClientSideException ile yapabiliriz
        //ancak orada method calsıyor ıksınınde farklı yerlerede farklı kullanımları mevcut.
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _service.GetByIdAsync(id);


            //if (product == null) //Bu yapı bast practices'e uygun olmadığı için servies katmanında yapıyoruz ve kednı exception sınıfımız ile yapıyoruzç
            //{
             //   return CreateActionResult(CustomResponseDTO<ProductDTO>.Fail(400, "Bu id'ye sahip bir ürün bulunamadı"));
            //}


            var productsDto = _mapper.Map<ProductDTO>(product);
            //return Ok(CustomResponseDTO<List<ProductDTO>>.Success(200,productsDtos)); //burayı CustoöbaseControllerde yapıyoruz.
            return CreateActionResult(CustomResponseDTO<ProductDTO>.Success(200, productsDto));
        }


        [HttpPost()]
        public async Task<IActionResult> Save(ProductDTO productDTO)
        {
            var product = await _service.addAsync(_mapper.Map<Product>(productDTO)); //gelen dtyu producta çeviriyoruz.
            var productsDto = _mapper.Map<ProductDTO>(product);    //sonra gerıyo dondurmek ıcın tekrar dtoya cevırıyoruz.
            //return Ok(CustomResponseDTO<List<ProductDTO>>.Success(200,productsDtos)); //burayı CustoöbaseControllerde yapıyoruz.
            return CreateActionResult(CustomResponseDTO<ProductDTO>.Success(201, productsDto));
        }


        [HttpPut()]
        public async Task<IActionResult> Update(ProductUpdateDTO productUpdateDTO)
        {
            await _service.UpdateAsync(_mapper.Map<Product>(productUpdateDTO)); //gelen dtyu producta çeviriyoruz.

            return CreateActionResult(CustomResponseDTO<NoContentDTO>.Success(204));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _service.GetByIdAsync(id);  //await ne araştır bir
            await _service.RemoveAsync(product);


            //if (product == null)    //bu yapıyı daha bestpractices yapacağız.
            //{
            //    return CreateActionResult(CustomResponseDTO<NoContentDTO>.Fail(404, "Bu id'ye sahip ürün bulunamadı"));
            //}
            return CreateActionResult(CustomResponseDTO<NoContentDTO>.Success(204));
        }

        [HttpGet("/app")]        //deneme
        public IActionResult get()
        {

           
            var y = _context.Categories.ToList();
            return Ok(y);
        }



        [HttpGet("[action]")] //action ile method adını alıyor.
        public async Task<IActionResult> GetProductWithCategory()
        {
            return CreateActionResult(await _service.GetProductWithCategory()); //DTO içeride yapıldı.
        }
    }
}
