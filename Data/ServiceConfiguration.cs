using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Data.Repositories.Contacts;
using Data.Repositories;
using Models;

namespace Data
{
    public static class ServiceConfiguration
    {
        public static void Configure(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<User>(options => { options.SignIn.RequireConfirmedAccount = false; })
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddScoped<IItemRepository, ItemRepository>();
        }
    }
}
