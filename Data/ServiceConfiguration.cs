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
using Data.Repositories.Contracts;

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

            //services.AddIdentity<User, IdentityRole>(options => { options.SignIn.RequireConfirmedAccount = false; })
            //    .AddEntityFrameworkStores<ApplicationDbContext>()
            //    .AddDefaultUI();

            services.AddScoped<IItemRepository, ItemRepository>();
            services.AddScoped<IContactRepository, ContactRepository>();

        }
    }
}
