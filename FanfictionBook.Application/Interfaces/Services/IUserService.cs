using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FanfictionBook.Application.DTOs.UserDTOs;

namespace FanfictionBook.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<ICollection<UserGetDto>> GetAllUsersAsync(CancellationToken cancellationToken);
        Task<int> CreateUserAsync(UserPostDto user, CancellationToken cancellationToken);
        Task<UserGetDto> UpdateUserAsync(UserUpdateDto newUser, CancellationToken cancellationToken);
    }
}
