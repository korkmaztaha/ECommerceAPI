﻿using ECommerceApi.Application.Repositories;
using ECommerceApi.Application.ViewModels;
using ECommerceApi.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        public ProductsController(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
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
        public async Task<IActionResult> Get()
        {
            return Ok(_productReadRepository.GetAll(false));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            return Ok(_productReadRepository.GetByIdAsync(id,false));
        }
        [HttpPost]
        public async Task<IActionResult> Post(VM_Create_Product model)
        {
            if(ModelState.IsValid)

            await _productWriteRepository.AddAsync(new()
            {
                Name = model.Name,
                Stock = model.Stock,
                Price = model.Price,
            });
            await _productWriteRepository.SaveAsync();
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

            return Ok();
        }
    }
}
