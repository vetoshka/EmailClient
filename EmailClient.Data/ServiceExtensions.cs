using System;
using System.Collections.Generic;
using System.Text;
using EmailClient.Data.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace EmailClient.Data
{
   public static class ServiceExtensions
    {
        public static void ConfigureRepository(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

    }
}
