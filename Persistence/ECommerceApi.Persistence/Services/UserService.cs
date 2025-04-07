using ECommerceApi.Application.Abstractions.Services;
using ECommerceApi.Application.DTOs.User;
using ECommerceApi.Application.Exceptions;
using ECommerceApi.Application.Helpers;
using ECommerceApi.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApi.Persistence.Services
{
    public class UserService : IUserService
    {
        readonly UserManager<Domain.Entities.Identity.AppUser> _userManager;

        public UserService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<CreateUserResponseDTO> CreateAsync(CreateUserDTO model)
        {
            IdentityResult result = await _userManager.CreateAsync(new()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = model.UserName,
                Email = model.Email,
                NameSurname = model.NameSurname,

            }, model.Password);

            CreateUserResponseDTO response = new() { Succeeded = result.Succeeded };

            if (result.Succeeded)
            {

                response.Message = "kullanıcı oluşturuldu";

            }

            return response;
        }

        public async Task UpdatePassword(string userId, string resetToken, string newPassoword)
        {
            AppUser user = await _userManager.FindByIdAsync(userId);
            if (userId != null)
            {
                resetToken = resetToken.UrlDecode();
                IdentityResult result = await _userManager.ResetPasswordAsync(user, resetToken, newPassoword);

                if (result.Succeeded)
                    await _userManager.UpdateSecurityStampAsync(user);
                else
                    throw new PasswordChangeFailedException();


            }
        }

        public async Task UpdateRefreshTokenAsync(string refreshToken, AppUser user, DateTime accessTokenDate, int addOnAccessTokenDate)
        {


            if (user != null)
            {
                user.RefreshToken = refreshToken;
                user.RefreshTokenEndDate = accessTokenDate.AddSeconds(addOnAccessTokenDate);
                await _userManager.UpdateAsync(user);
            }
            else
                throw new NotFoundUserException();


        }
    }
}
