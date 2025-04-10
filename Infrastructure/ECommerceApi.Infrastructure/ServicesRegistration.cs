using ECommerceApi.Application.Abstractions.Services;
using ECommerceApi.Application.Abstractions.Services.Configurations;
using ECommerceApi.Application.Abstractions.Storage;
using ECommerceApi.Application.Abstractions.Token;
using ECommerceApi.Application.Repositories;
using ECommerceApi.Infrastructure.Enums;
using ECommerceApi.Infrastructure.Services;
using ECommerceApi.Infrastructure.Services.Configurations;
using ECommerceApi.Infrastructure.Services.Storage;
using ECommerceApi.Infrastructure.Services.Storage.Azure;
using ECommerceApi.Infrastructure.Services.Storage.Local;
using ECommerceApi.Infrastructure.Services.Token;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApi.Infrastructure
{
    public static class ServicesRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IStorageService, StorageService>();
            services.AddScoped<ITokenHandler, TokenHandler>();
            services.AddScoped<IMailService, MailService>();
            services.AddScoped<IApplicationService, ApplicationService>();


        }
        public static void AddStorage<T>(this IServiceCollection service) where T : class, IStorage
        {
            service.AddScoped<IStorage,T>();
        }
        public static void AddStorage(this IServiceCollection serviceCollection, StorageType storageType)
        {
            switch (storageType)
            {
                case StorageType.Local:
                    serviceCollection.AddScoped<IStorage, LocalStorage>();
                    break;
                case StorageType.Azure:
                    serviceCollection.AddScoped<IStorage, AzureStorage>();
                    break;
                case StorageType.AWS:

                    break;
                default:
                    serviceCollection.AddScoped<IStorage, LocalStorage>();
                    break;
            }
        }
    }
}
