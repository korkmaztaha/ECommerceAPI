using ECommerceApi.Application.Features.Commands.CreateProduct;
using ECommerceApi.Application.Features.Queries.GetAllProduct;
using ECommerceApi.Application.Repositories;
using ECommerceApi.Application.RequestParameters;
using ECommerceApi.Application.ViewModels;
using ECommerceApi.Domain.Entities;
using ECommerceApi.Persistence.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Xml.Linq;

namespace ECommerceApi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        readonly private IProductWriteRepository _productWriteRepository;
        readonly private IProductReadRepository _productReadRepository;
        readonly private IWebHostEnvironment _webHostEnvironment;
        //readonly private IFileService _fileService;


        readonly IMediator _mediator;
        public ProductsController(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository, IWebHostEnvironment webHostEnvironment /*IFileService fileService*/, IMediator mediator)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
            _webHostEnvironment = webHostEnvironment;
            _mediator = mediator;
            //_fileService = fileService;
        }

        //[HttpGet]
        //public async Task Get()
        //{
        //    await _productWriteRepository.AddAsync(new() 
        //    { 
        //        Id = Guid.NewGuid(), Name = "p99", Price = 11, CreatedDate = DateTime.UtcNow, Stock = 22 }
        //    );


        //    await _productWriteRepository.SaveAsync();

        //    //Product p = await _productReadRepository.GetByIdAsync("3c319c26-d0ed-4340-82e8-34b3296b89be");
        //    //p.Name = "güncellendiyeni";
        //    //await _productWriteRepository.SaveAsync();


        //}
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]GettAllProductQueryRequest gettAllProductQueryRequest)
        {
            GettAllProductQueryResponse response = await _mediator.Send(gettAllProductQueryRequest);
            return Ok(response);



        }
        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetById(string id)
        //{
        //    return Ok(_productReadRepository.GetByIdAsync(id, false));
        //}
        [HttpPost]
        public async Task<IActionResult> Post(CreateProductCommandRequest request)
        {
          CreateProductCommandResponse response = await _mediator.Send(request);
            return StatusCode((int)HttpStatusCode.Created);

           
        }

        [HttpPut]
        public async Task<IActionResult> Put(VM_Update_Product model)
        {
            Product product = await _productReadRepository.GetByIdAsync(model.Id);

            product.Name = model.Name;
            product.Stock = model.Stock;
            product.Price = model.Price;

            await _productWriteRepository.SaveAsync();
            return StatusCode((int)HttpStatusCode.Created);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _productWriteRepository.RemoveAsync(id);
            await _productWriteRepository.SaveAsync();

            return Ok(new
            {
                message = "silme işlemi başarılı"
            });
        }

        //[HttpPost("[action]")]
        //public async Task<IActionResult> Upload()
        //{

        //    await _fileService.UploadAsync("resource/product-images", Request.Form.Files);

        //    //string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "resource/product-images");
        //    //if (!Directory.Exists(uploadPath))
        //    //    Directory.CreateDirectory(uploadPath);

        //    //Random random = new Random();

        //    //foreach (IFormFile file in Request.Form.Files)
        //    //{
        //    //    string fullPath = Path.Combine(uploadPath, $"{random.Next()}{Path.GetExtension(file.FileName)}");
        //    //    using FileStream fileStream = new(fullPath, FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024);
        //    //    await file.CopyToAsync(fileStream);
        //    //    await fileStream.FlushAsync();
        //    //}
        //    return Ok();

        //}
        //[HttpPost("Upload")]
        //public async Task<IActionResult> Upload([FromForm] IFormFile file)
        //{
        //    if (file == null || file.Length == 0)
        //    {
        //        return BadRequest("Dosya seçilmedi.");
        //    }

        //    string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "resource/product-images");
        //    if (!Directory.Exists(uploadPath))
        //        Directory.CreateDirectory(uploadPath);

        //    string uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
        //    string fullPath = Path.Combine(uploadPath, uniqueFileName);

        //    using FileStream fileStream = new(fullPath, FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024);
        //    await file.CopyToAsync(fileStream);
        //    await fileStream.FlushAsync();

        //    return Ok(new { FilePath = $"resource/product-images/{uniqueFileName}" });
        //}
    }

}



