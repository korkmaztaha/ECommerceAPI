using ECommerceApi.Application.Features.Commands.AppUser.CreateUser;
using ECommerceApi.Application.Features.Commands.AppUser.FacebookLogin;
using ECommerceApi.Application.Features.Commands.AppUser.GoogleLogin;
using ECommerceApi.Application.Features.Commands.AppUser.LoginUser;
using ECommerceApi.Application.Features.Commands.AppUser.UpdatePassword;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceApi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserCommandRequest request)
        {
            CreateUserCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginUserCommandRequest request)
        {
            LoginUserCommandResponse response=await _mediator.Send(request);
          
            return Ok(response);
        }
        [HttpPost("google-login")]
        public async Task<IActionResult> GoogleLogin(GoogleLoginCommandRequest request)
        {
            GoogleLoginCommandResponse response = await _mediator.Send(request);

            return Ok(response);
        }

        [HttpPost("facebook-login")]
        public async Task<IActionResult> FacebookLogin(FacebookLoginCommandRequest request)
        {
            FacebookLoginCommandResponse response = await _mediator.Send(request);

            return Ok(response);
        }
        [HttpPost("update-password")]
        public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordCommandRequest request)
        {
            UpdatePasswordCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }

    }
}
