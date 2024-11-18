using AuthenticationApi.Application.DTOs;
using AuthenticationApi.Application.Interfaces;
using eCommerce.SharedLibrary.Response;
using Microsoft.Extensions.Configuration;

namespace AuthenticationApi.Infrastructure.Repositories
{
    public class UserRepository (IUser userInterface,IConfiguration config): IUser
    {
        public Task<Response> GetUser(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<Response> Login(LoginDTO loginDTO)
        {
            throw new NotImplementedException();
        }

        public Task<Response> Register(AppUserDTO appUserDTO)
        {
            throw new NotImplementedException();
        }
    }
}
