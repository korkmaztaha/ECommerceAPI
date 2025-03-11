using ECommerceApi.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApi.Application.Abstractions.Services.Authentications
{
    public interface IExternalAuthentication
    {
        Task<TokenDTO> FacebookLoginAsync(string authToken, int accessTokenLifeTime);
        Task<TokenDTO> GoogleLoginAsync(string idToken, int accessTokenLifeTime);
    }
}
