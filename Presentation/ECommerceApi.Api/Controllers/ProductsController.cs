using ECommerceApi.Application.Repositories;
using ECommerceApi.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace ECommerceApi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        readonly private IProductWriteRepository _productWriteRepository;
        readonly private IProductReadRepository _productReadRepository;

        public ProductsController(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
        }

        [HttpGet]
        public async Task Get()
        {
            await _productWriteRepository.AddAsync(new() 
            { 
                Id = Guid.NewGuid(), Name = "p99", Price = 11, CreatedDate = DateTime.UtcNow, Stock = 22 }
            );


            await _productWriteRepository.SaveAsync();

            //Product p = await _productReadRepository.GetByIdAsync("3c319c26-d0ed-4340-82e8-34b3296b89be");
            //p.Name = "güncellendiyeni";
            //await _productWriteRepository.SaveAsync();


        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBtId(string id)
        {
            Product product= await _productReadRepository.GetByIdAsync(id);
            return Ok(product);
         


        }
    }
}
