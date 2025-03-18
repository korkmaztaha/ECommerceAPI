using ECommerceApi.Application.Abstractions.Services;
using ECommerceApi.Application.DTOs.Order;
using ECommerceApi.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApi.Persistence.Services
{
    public class OrderService : IOrderService
    {
        readonly IOrderWriteRepository _orderWriteRepository;

        public OrderService(IOrderWriteRepository orderWriteRepository)
        {
            _orderWriteRepository = orderWriteRepository;
        }

        public async Task CreateOrderAsync(CreateOrderDTO createOrder)
        {
            await _orderWriteRepository.AddAsync(new()
            {
                Adress=createOrder.Address,
                Id=Guid.Parse(createOrder.BasketId),
                Description=createOrder.Description,

            });
            await _orderWriteRepository.SaveAsync();
        }
    }
}
