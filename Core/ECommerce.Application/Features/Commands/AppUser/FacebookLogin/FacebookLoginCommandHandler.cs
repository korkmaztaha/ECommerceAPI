using ECommerceApi.Application.Abstractions.Token;
using ECommerceApi.Application.DTOs.Facebook;
using Google.Apis.Auth.OAuth2;
using MediatR;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ECommerceApi.Application.Features.Commands.AppUser.FacebookLogin
{
    public class FacebookLoginCommandHandler : IRequestHandler<FacebookLoginCommandRequest, FacebookLoginCommandResponse>
    {
        readonly UserManager<Domain.Entities.Identity.AppUser> _userManager;
        readonly ITokenHandler _tokenHandler;
        readonly HttpClient _httpClient;
        readonly IConfiguration _configuration;


        public FacebookLoginCommandHandler(UserManager<Domain.Entities.Identity.AppUser> userManager, ITokenHandler tokenHandler, IHttpClientFactory httpClientFactory)
        {
            _userManager = userManager;
            _tokenHandler = tokenHandler;
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task<FacebookLoginCommandResponse> Handle(FacebookLoginCommandRequest request, CancellationToken cancellationToken)
        {
            string accessTokenResponse = await _httpClient.GetStringAsync(_configuration["Token:FacebookTokenURL"]);

            FacebookAccessTokenResponseDTO response = JsonSerializer.Deserialize<FacebookAccessTokenResponseDTO>(accessTokenResponse);

            string userAccessTokenValidation= await _httpClient.GetStringAsync($"https://graph.facebook.com/debug_token?input_token={request.AuthToken}&access_token={response.AccessToken}");

            FacebookUserAccessTokenValidationDTO validation = JsonSerializer.Deserialize<FacebookUserAccessTokenValidationDTO>(userAccessTokenValidation);

            if (validation?.Data.IsValid != null)
            {
                string userInfoResponse = await _httpClient.GetStringAsync($"https://graph.facebook.com/me?fields=email,name&access_token={request.AuthToken}");

                FacebookUserInfoResponseDTO? userInfo = JsonSerializer.Deserialize<FacebookUserInfoResponseDTO>(userInfoResponse);

                var info = new UserLoginInfo("FACEBOOK", validation.Data.UserId, "FACEBOOK");
                Domain.Entities.Identity.AppUser user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);

                //return await CreateUserExternalAsync(user, userInfo.Email, userInfo.Name, info, accessTokenLifeTime);
            }
            return new();


        }
    }
}
