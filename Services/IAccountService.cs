﻿using RestaurantApi.Models;

namespace RestaurantApi.Services
{
    public interface IAccountService
    {
        void RegisterUser(RegisterUserDto dto);
    }
}