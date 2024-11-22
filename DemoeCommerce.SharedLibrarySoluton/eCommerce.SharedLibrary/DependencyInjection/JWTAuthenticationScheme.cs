﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;


namespace eCommerce.SharedLibrary.DependencyInjection
{
    public static class JWTAuthenticationScheme
    {
        public static IServiceCollection AddJWTAuthenticationScheme(this IServiceCollection services, IConfiguration config)
        {
            // AddJWT Services
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    var key = Encoding.UTF8.GetBytes(config.GetSection("Authentication:Key").Value!);
                    string issuer = config.GetSection("Authentication:Issuer").Value!;

                    string audience = config.GetSection("Authentication: Audience").Value!;
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = false,
                        ValidateIssuerSigningKey = true,
                        ValidAudience = audience,
                        IssuerSigningKey = new SymmetricSecurityKey(key),

                    };


                });
            return services;
        }
    }

}

//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.IdentityModel.Tokens;
//using System.Text;

//namespace eCommerce.SharedLibrary.DependencyInjection
//{
//    public static class JWTAuthenticationScheme
//    {
//        public static IServiceCollection AddJWTAuthenticationScheme(this IServiceCollection services, IConfiguration config)
//        {
//            // Add JWT Services
//            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
//                {
//                    var key = Encoding.UTF8.GetBytes(config["Authentication:Key"]!);
//                    string issuer = config["Authentication:Issuer"]!;
//                    string audience = config["Authentication:Audience"]!;

//                    options.RequireHttpsMetadata = false;
//                    options.TokenValidationParameters = new TokenValidationParameters
//                    {
//                        ValidateIssuer = true,
//                        ValidateAudience = true,
//                        ValidateLifetime = true, // Enable lifetime validation
//                        ValidateIssuerSigningKey = true,
//                        ValidIssuer = issuer,
//                        ValidAudience = audience,
//                        IssuerSigningKey = new SymmetricSecurityKey(key)
//                    };
//                });

//            return services;
//        }
//    }
//}

