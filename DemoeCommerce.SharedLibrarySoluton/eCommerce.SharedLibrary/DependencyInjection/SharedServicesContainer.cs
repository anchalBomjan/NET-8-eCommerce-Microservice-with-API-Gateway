using eCommerce.SharedLibrary.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;
namespace eCommerce.SharedLibrary.DependencyInjection
{
    public   static class SharedServicesContainer
    {
        public static IServiceCollection AddSharedServices<TContext>
            (this IServiceCollection services,IConfiguration config, string fileName) where TContext : DbContext
        {
            //Add Generic Database Context
            services.AddDbContext<TContext>(option => option.UseSqlServer(
                config
                .GetConnectionString("eCommerceConnection"), sqlserverOption =>
                sqlserverOption.EnableRetryOnFailure()));


            //configure serilog logging
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.Debug()
                .WriteTo.Console()
                .WriteTo.File(
                   path: $"{fileName}-.txt",
                   rollingInterval: RollingInterval.Day,
                   restrictedToMinimumLevel: LogEventLevel.Information,
          
                   outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}"


                )
                .CreateLogger();


            //Add JWT Authentication Scheme
            JWTAuthenticationScheme.AddJWTAuthenticationScheme(services, config);

             return services;
        }
          
        public static IApplicationBuilder UserSharedPolices(this IApplicationBuilder app)
        {
            //Use global Exception
            app.UseMiddleware<GlobalException>();
            //Refister middleware to block  all the outsiders API Calls

           // app.UseMiddleware<ListenToOnlyApiGateway>();
            return app;
        }
    }
}
