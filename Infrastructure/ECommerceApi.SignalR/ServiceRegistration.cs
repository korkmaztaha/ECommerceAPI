using ECommerceApi.Application.Abstractions.Hubs;
using ECommerceApi.SignalR.HubServices;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApi.SignalR
{
    public static class ServiceRegistration
    {
        public static void AddSignalRServices(this IServiceCollection services)
        {

            services.AddTransient<IProductHubService, ProductHubService>();
            services.AddSignalR();


        }
    }
}
