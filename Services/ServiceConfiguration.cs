using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Services
{
    public static class ServiceConfiguration
    {
        public static void Configure(IServiceCollection services, IConfiguration configuration)
        {
            Data.ServiceConfiguration.Configure(services, configuration);

            //services.AddScoped<IItemService, ItemService>();
        }
    }
}
