using ECommerceApi.Application.Features.Commands.AuthorizationEndpoint.AssignRoleEndpoint;
using ECommerceApi.Application.Features.Queries.AuthorizationEndpoint.GetRolesToEndpoint;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceApi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationEndpointsController : ControllerBase
    {
        readonly IMediator _mediator;

        public AuthorizationEndpointsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> GetRolesToEndpoint(GetRolesToEndpointQueryRequest request)
        {
            GetRolesToEndpointQueryResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> AssignRoleEndpoint(AssignRoleEndpointCommandRequest request)
        {
            request.Type = typeof(Program);
            AssignRoleEndpointCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}

