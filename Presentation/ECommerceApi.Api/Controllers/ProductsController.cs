using ECommerceApi.Application.Features.Commands.Products.CreateProduct;
using ECommerceApi.Application.Features.Commands.Products.RemoveProduct;
using ECommerceApi.Application.Features.Commands.Products.UpdateProduct;
using ECommerceApi.Application.Features.Queries.Products.GetAllProduct;
using ECommerceApi.Application.Features.Queries.Products.GetByIdProduct;
using ECommerceApi.Application.Repositories;
using ECommerceApi.Application.RequestParameters;
using ECommerceApi.Application.ViewModels;
using ECommerceApi.Domain.Entities;
using ECommerceApi.Persistence.Repositories;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Xml.Linq;

namespace ECommerceApi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes="Admin")]
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

       
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]GettAllProductQueryRequest request)
        {
            GettAllProductQueryResponse response = await _mediator.Send(request);
            return Ok(response);



        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById([FromRoute]GetByIdProductQueryRequest request)
        {
            GetByIdProductQueryResponse response =await _mediator.Send(request);
            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> Post(CreateProductCommandRequest request)
        {
          CreateProductCommandResponse response = await _mediator.Send(request);
            return StatusCode((int)HttpStatusCode.Created);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody]UpdateProductCommandRequest request)
        {
           UpdateProductCommandResponse response=await _mediator.Send(request);
            return Ok();
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete([FromRoute] RemoveProductCommandRequest request)
        {
            RemoveProductCommandResponse response = await _mediator.Send(request);
            return Ok();
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



