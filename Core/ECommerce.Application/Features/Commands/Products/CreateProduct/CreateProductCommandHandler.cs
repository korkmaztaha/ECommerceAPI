using ECommerceApi.Application.Abstractions.Hubs;
using ECommerceApi.Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApi.Application.Features.Commands.Products.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest, CreateProductCommandResponse>
    {
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly IProductHubService _productHubService;
        public CreateProductCommandHandler(IProductWriteRepository repository, IProductHubService productHubService)
        {
            _productWriteRepository = repository;
            _productHubService = productHubService;
        }

        public async Task<CreateProductCommandResponse> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {
            await _productWriteRepository.AddAsync(new()
            {
                Name = request.Name,
                Stock = request.Stock,
                Price = request.Price,
            });
            await _productWriteRepository.SaveAsync();
            _productHubService.ProductAddedMessageAsync($"{request.Name} ürünü eklendi");
            return new();
        }
    }
}
