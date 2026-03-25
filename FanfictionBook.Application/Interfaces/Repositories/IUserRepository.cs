using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FanfictionBook.Domain.Entities;

namespace FanfictionBook.Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<int> CreateUserAsync(UserEntity user, string password, CancellationToken cancellationToken);
        Task<ICollection<UserEntity>> GetAllUsersAsync(CancellationToken cancellationToken);
        Task<UserEntity> GetUserByEmailAsync(string email, CancellationToken cancellationToken);
        Task<UserEntity> GetUserByIdAsync(int id,  CancellationToken cancellationToken);
        Task<UserEntity> UpdateUserAsync(UserEntity newUser, string? newPassword, CancellationToken cancellationToken);
        Task DeleteUserAsync(int id, CancellationToken cancellationToken);
        Task SaveChanges(CancellationToken cancellationToken);
    }
}
