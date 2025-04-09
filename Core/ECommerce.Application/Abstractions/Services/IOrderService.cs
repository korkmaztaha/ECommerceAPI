using ECommerceApi.Application.DTOs.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApi.Application.Abstractions.Services
{
    public interface IOrderService
    {
        Task CreateOrderAsync(CreateOrderDTO createOrder);
        Task<ListOrderDTO> GetAllOrdersAsync(int page, int size);
        Task<SingleOrderDTO> GetOrderByIdAsync(string id);

        Task<(bool, CompletedOrderDTO)> CompleteOrderAsync(string id);


    }
}
