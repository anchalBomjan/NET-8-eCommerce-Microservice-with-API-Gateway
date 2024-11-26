using eCommerce.SharedLibrary.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderApi.Application.Interfaces;
using OrderApi.Infrastructure.Data;
using OrderApi.Infrastructure.Repositories;
namespace OrderApi.Infrastructure.DependencyInjection
{
    public static class ServiceContainer
    {
        public static IServiceCollection AddInfrastructureService(this IServiceCollection services, IConfiguration config) 
        {
            //Add Database Connectivity
            //Add authentication scheme
            SharedServicesContainer.AddSharedServices<OrderDbContext>(services, config ,config["MySerilog:FileName"]!);

            //create Dependency Injection
            services.AddScoped<IOrder, OrderRepository>();
            return services;

        }

        public static  IApplicationBuilder UserInfrastructurePolicy(this IApplicationBuilder app)
        {
            //Registration middleware
            //Globalexcepstion -> handle external errors
            // ListenTo APIGateway  only -> block all outersiders calls

           // SharedServicesContainer.UserSharedPolices(app);
            return app;

        }
    }
}
