using ECommerceApi.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApi.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersitenceServices(this IServiceCollection services)
        {
            services.AddDbContext<ECommerceAPIDbContext>(options=> options.UseNpgsql(Configuration.ConnectionString));
           
        }

    }
}
