using ECommerceApi.Application.Abstractions.Services;
using ECommerceApi.Application.DTOs.Order;
using ECommerceApi.Application.Repositories;
using Microsoft.EntityFrameworkCore;
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
        readonly IOrderReadRepository _orderReadRepository;

        public OrderService(IOrderWriteRepository orderWriteRepository, IOrderReadRepository orderReadRepository)
        {
            _orderWriteRepository = orderWriteRepository;
            _orderReadRepository = orderReadRepository;
        }

        public async Task CreateOrderAsync(CreateOrderDTO createOrder)
        {
            var orderCode = (new Random().NextDouble() * 1000).ToString();
            orderCode = orderCode.Substring(orderCode.IndexOf(".")+1, orderCode.Length - orderCode.IndexOf(".") - 1);
            await _orderWriteRepository.AddAsync(new()
            {
                Adress = createOrder.Address,
                Id = Guid.Parse(createOrder.BasketId),
                Description = createOrder.Description,
                OrderCode = orderCode,

            });
            await _orderWriteRepository.SaveAsync();
        }

        public Task<ListOrderDTO> GetAllOrdersAsync(int page, int size)
        {
            throw new NotImplementedException();
        }
    }
}
