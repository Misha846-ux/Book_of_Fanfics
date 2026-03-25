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
        Task<UserGetDto> GetUserByEmailAsync(string email, CancellationToken cancellationToken);
        Task<int> CreateUserAsync(UserPostDto user, CancellationToken cancellationToken);
        Task<UserGetDto> UpdateUserAsync(UserUpdateDto newUser, CancellationToken cancellationToken);
        Task<string> LoginWithPasswordAsync(UserLoginDto userLogin, CancellationToken cancellationToken);
        Task<string> LoginWithRecoveryTokenAsync(UserLoginDto userLogin, CancellationToken cancellationToken);
        Task<string> CreateRecoveryTokenAsync(string userEmail, CancellationToken cancellationToken);

    }
}
