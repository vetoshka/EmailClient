using System;
using System.Collections.Generic;
using System.Text;
using EmailClient.Data.Entities;
using EmailClient.Data.Interfaces;
using EmailClient.Data.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace EmailClient.Data
{
   public static class ServiceExtensions
    {
        public static void ConfigureRepository(this IServiceCollection services)
        {
            services.AddScoped<IRepository<MailBoxProperties>, AccountRepository>();
            services.AddScoped<IEmailMessageRepository, EmailMessageRepository>();
        }

    }
}
