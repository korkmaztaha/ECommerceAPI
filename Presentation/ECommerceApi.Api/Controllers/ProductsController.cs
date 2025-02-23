using ECommerceApi.Application.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
            await _productWriteRepository.AddRangeAsync(new()
            {
                new(){Id=Guid.NewGuid(), Name="p4",Price=11, CreatedDate=DateTime.UtcNow, Stock=22},
                new(){Id=Guid.NewGuid(), Name="p5",Price=21, CreatedDate=DateTime.UtcNow, Stock=32},
                new(){Id=Guid.NewGuid(), Name="p6",Price=31, CreatedDate=DateTime.UtcNow, Stock=42},
            });
           await _productWriteRepository.SaveAsync();


        }
    }
}
