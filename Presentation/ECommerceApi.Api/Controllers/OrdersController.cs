using ECommerceApi.Application.Features.Commands.Order.CreateOrder;
using ECommerceApi.Application.Features.Queries.Orders.GetAllOrders;
using ECommerceApi.Application.Features.Queries.Orders.GetOrderById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceApi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class OrdersController : ControllerBase
    {
        readonly IMediator _mediator;
        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]

        public async Task<ActionResult> CreateOrder(CreateOrderCommandRequest request)
        {
            CreateOrderCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpGet]

        public async Task<ActionResult> GetAllOrders([FromQuery] GetAllOrdersQueryRequest request)
        {
            GetAllOrdersQueryResponse response = await _mediator.Send(request);
            return Ok(response);
        }
        [HttpGet("{Id}")]
        public async Task<ActionResult> GetOrderById([FromRoute] GetOrderByIdQueryRequest request)
        {
            GetOrderByIdQueryResponse response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
