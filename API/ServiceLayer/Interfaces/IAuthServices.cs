﻿using ModelLayer.Contracts.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interfaces
{
    public interface IAuthServices
    {
        Task<RegisterResponse> Register(RegisterRequest registerRequest);

        Task<LoginResponse> Login(LoginRequest loginRequest);
    }
}
