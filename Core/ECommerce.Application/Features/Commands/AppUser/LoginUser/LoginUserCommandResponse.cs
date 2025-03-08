using ECommerceApi.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApi.Application.Features.Commands.AppUser.LoginUser
{
    public class LoginUserCommandResponse
    {

    }
    public class LoginUserSuccessCommandResponse : LoginUserCommandResponse
    {
        public TokenDTO Token { get; set; }
    }
    public class LoginUserErrorCommandResponse : LoginUserCommandResponse
    {
        public string Message { get; set; }
    }
}
