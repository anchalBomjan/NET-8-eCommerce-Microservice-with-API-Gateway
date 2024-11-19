using AuthenticationApi.Application.Interfaces;
using AuthenticationApi.Infrastructure.Data;
using AuthenticationApi.Infrastructure.Repositories;
using eCommerce.SharedLibrary.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationApi.Infrastructure.DependencyInjection
{
    public  static class ServiceContainer
    {

        public static IServiceCollection AddInfrastructureService(this IServiceCollection services, IConfiguration config)
        {
            //add database connection
            //add authentication Scheme

           //Creating dependency injection
            SharedServicesContainer.AddSharedServices<AuthenticationDbContext>(services, config, config["MySerilog:FileName"]!);
            services.AddScoped<IUser, UserRepository>();
            return services;

        }
        public static IApplicationBuilder UserInterfaceInfrastructurePolicy( this IApplicationBuilder app)
        {
            //Register middleware
            //Global Exception:Handle external errors
            //Listen only to Api GateWay:Block all outersider call.

            SharedServicesContainer.UserSharedPolices(app);
            return app;
        }


    }
}
