﻿using ECommerceApi.Application.Repositories;
using ECommerceApi.Application.Services;
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
            //services.AddScoped<IFileService, FileService>();

        }
    }
}
