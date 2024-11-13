using eCommerce.SharedLibrary.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductApi.Application.Interfaces;
using ProductApi.Infrastructure.Data;
using ProductApi.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductApi.Infrastructure.DependencyInjection
{
    public static  class ServiceContainer
    {
         public static IServiceCollection AddInfrastructureService(this IServiceCollection services,IConfiguration config)
        {
            //Adding databse connectivity
            //Addinmg authentication scheme
            SharedServicesContainer.AddSharedServices<ProductDbContext>(services, config, config["MySerilog:FineName"]!);

            //Create Dependency injection ((DI)
            services.AddScoped<IProduct, ProductRepository>();

            return services;

        }

        public static IApplicationBuilder UseInfrastructurePolicy(this IApplicationBuilder app)
        {
            //Register middleware such as:
            //Global Exception:handles  external errors
            //Listen  to Only API GateWay: Blocks all  outersider calls
            SharedServicesContainer.UserSharedPolices(app);
            return app;


        
        }
    }
}
