using ECommerceApi.Application.Repositories;
using ECommerceApi.Application.RequestParameters;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApi.Application.Features.Queries.Products.GetAllProduct
{
    public class GettAllProductQueryHandler : IRequestHandler<GettAllProductQueryRequest, GettAllProductQueryResponse>
    {
        readonly IProductReadRepository _productReadRepository;

        public GettAllProductQueryHandler(IProductReadRepository productReadRepository)
        {
            _productReadRepository = productReadRepository;
        }


        public async Task<GettAllProductQueryResponse> Handle(GettAllProductQueryRequest request, CancellationToken cancellationToken)
        {
            var query = _productReadRepository.GetAll(false);

            var totalCount = await query.CountAsync(cancellationToken);

            var products = await query
                .OrderBy(p => p.Id)
                .Skip(request.Page * request.Size)
                .Take(request.Size)
                .Select(p => new
                {
                    p.Id,
                    p.Name,
                    p.Price,
                    p.Stock
                })
                .ToListAsync(cancellationToken);

            return new GettAllProductQueryResponse
            {
                Products = products,
                TotalCount = totalCount
            };



        }


    }
}
