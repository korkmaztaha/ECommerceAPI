using ECommerceApi.Application.Abstractions.Services;
using ECommerceApi.Application.ViewModels.Baskets;
using ECommerceApi.Domain.Entities;
using ECommerceApi.Domain.Entities.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApi.Persistence.Services
{
    public class BasketService : IBasketService
    {
        readonly IHttpContextAccessor _contextAccessor;
        readonly UserManager<AppUser> _userManager;

        public BasketService(IHttpContextAccessor contextAccessor, UserManager<AppUser> userManager)
        {
            _contextAccessor = contextAccessor;
            _userManager = userManager;
        }

        private async Task<AppUser> ContextUser()
        {
            var userName = _contextAccessor.HttpContext.User.Identity.Name;
            if (!string.IsNullOrEmpty(userName))
            {
                AppUser user = await _userManager.Users
                     .Include(u => u.Baskets)
                     .FirstOrDefaultAsync(u => u.UserName == userName);

            }
            return new();
        }

        public Task AddItemToBasketAsync(VM_Create_BasketItem basketItem)
        {
            throw new NotImplementedException();
        }

        public Task<List<BasketItem>> GetBasketItemsAsync()
        {
            throw new NotImplementedException();
        }

        public Task RemoveBasketItemAsync(string basketItemId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateQuantityAsync(VM_Update_BasketItem basketItem)
        {
            throw new NotImplementedException();
        }
    }
}
