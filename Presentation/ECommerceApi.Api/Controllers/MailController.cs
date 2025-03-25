using ECommerceApi.Application.Abstractions.Services;
using ECommerceApi.Application.Features.Queries.Orders.GetAllOrders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceApi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = "Admin")]
    public class MailController : ControllerBase
    {
        readonly IMailService _mailService;

        public MailController(IMailService mailService)
        {
            _mailService = mailService;
        }

        [HttpGet]
        public async Task<ActionResult> MailTest()
        {
           await _mailService.SendMessageAsync("xxx@mail.com", "Test mail Subject", "TestMail Body");
            return Ok();
        }
    }
}
