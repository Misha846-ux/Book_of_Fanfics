using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FanfictionBook.Application.Interfaces.Helpers;
using FanfictionBook.Application.Interfaces.Repositories;
using FanfictionBook.Domain.Entities;
using FanfictionBook.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FanfictionBook.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly FanfictionBookContext _context;
        private readonly IHashHelper _hashHelper;
        public UserRepository(FanfictionBookContext context, IHashHelper hashHelper)
        {
            _context = context;
            _hashHelper = hashHelper;
        }

        public async Task<int> CreateUserAsync(UserEntity user, string password, CancellationToken cancellationToken)
        {
            try
            {
                user.PasswordHash = _hashHelper.HashPassword(password);
                await _context.Users.AddAsync(user, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                return user.Id;
            }
            catch (OperationCanceledException oex)
            {
                throw new Exception("User Repository: CreateUserAsync operation were canceled");
            }
            catch (Exception ex)
            {
                throw new Exception("User Repository: Problem with CreateUserAsync");
            }
        }

        public Task DeleteUserAsync(int id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<UserEntity>> GetAllUsersAsync(CancellationToken cancellationToken)
        {
            try
            {
                return await _context.Users
                    .Include(u => u.Fanfics)
                    .Include(u => u.Comments)
                    .ToListAsync(cancellationToken);
            }
            catch (OperationCanceledException oex)
            {
                throw new Exception("User Repository: GetAllUsersAsync operation were canceled");
            }
            catch (Exception ex)
            {
                throw new Exception("User Repository: Problem with GetAllUsersAsync");
            }
        }

        public async Task<UserEntity> GetUserByEmailAsync(string email, CancellationToken cancellationToken)
        {
            try
            {
                return await _context.Users
                    .Include(u => u.Fanfics)
                    .Include(u => u.Comments)
                    .SingleOrDefaultAsync(u => u.Email == email, cancellationToken);
            }
            catch (OperationCanceledException oex)
            {
                throw new Exception("User Repository: GetUserByEmailAsync operation were canceled");
            }
            catch (Exception ex)
            {
                throw new Exception("User Repository: Problem with GetUserByEmailAsync");
            }
        }

        public async Task<UserEntity> GetUserByIdAsync(int id, CancellationToken cancellationToken)
        {
            try
            {
                return await _context.Users
                    .Include(u => u.Fanfics)
                    .Include(u => u.Comments)
                    .SingleOrDefaultAsync(u => u.Id == id, cancellationToken);
            }
            catch (OperationCanceledException oex)
            {
                throw new Exception("User Repository: GetUserByIdAsync operation were canceled");
            }
            catch (Exception ex)
            {
                throw new Exception("User Repository: Problem with GetUserByIdAsync");
            }
        }

        public async Task<UserEntity> UpdateUserAsync(UserEntity newUser, string? newPassword, CancellationToken cancellationToken)
        {
            try
            {
                if(newPassword != null)
                {
                    newUser.PasswordHash = _hashHelper.HashPassword(newPassword);
                }
                UserEntity user = await GetUserByIdAsync(newUser.Id, cancellationToken);
                if(user  == null)
                {
                    throw new Exception();
                }
                PropertyInfo[] properties = typeof(UserEntity).GetProperties();
                foreach(PropertyInfo prop in properties)
                {
                    var value = prop.GetValue(newUser);
                    if(value != null)
                    {
                        prop.SetValue(user, value);
                    }
                }
                await _context.SaveChangesAsync(cancellationToken);
                return user;
            }
            catch (OperationCanceledException oex)
            {
                throw new Exception("User Repository: UpdateUserAsync operation were canceled");
            }
            catch (Exception ex)
            {
                throw new Exception("User Repository: Problem with UpdateUserAsync");
            }
        }
    }
}
