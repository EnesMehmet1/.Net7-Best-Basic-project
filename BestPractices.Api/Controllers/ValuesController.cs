using AutoMapper;
using BestPractices.Core.Entities;
using BestPractices.Core.Services;
using BEstPractices.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BestPractices.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IService<Product> _service;
        AppDbContext _context;
        public ValuesController(IMapper mapper, IService<Product> service, AppDbContext context)
        {
            _context = context;
            _mapper = mapper;
            _service = service;
        }


    }
}
