﻿

using AuthenticationApi.Application.DTOs;
using eCommerce.SharedLibrary.Response;

namespace AuthenticationApi.Application.Interfaces
{
    public  interface IUser
    {
        Task<Response> Register(AppUserDTO appUserDTO);
        Task<Response> Login(LoginDTO loginDTO);
        Task<GetUserDTO> GetUser(int userId);

    }
}
