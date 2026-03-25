using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using FanfictionBook.Application.DTOs.UserDTOs;

namespace FanfictionBook.Application.Interfaces.Services
{
    public interface IJwtService
    {
        string GenerateAccessToken(UserLoginDto userLoginDto, string role);
        ClaimsPrincipal? DecodeToken(string token);
    }
}
