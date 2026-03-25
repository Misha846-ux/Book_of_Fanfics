using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FanfictionBook.Application.DTOs.UserDTOs;
using FanfictionBook.Application.Interfaces.Helpers;
using FanfictionBook.Application.Interfaces.Repositories;
using FanfictionBook.Application.Interfaces.Services;
using FanfictionBook.Domain.Entities;

namespace FanfictionBook.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IHashHelper _hashHelper;
        private readonly IJwtService _jwtService;
        public UserService(IMapper mapper,  IHashHelper hashHelper, IUserRepository userRepository
            , IJwtService jwtService)
        {
            _hashHelper = hashHelper;
            _mapper = mapper;
            _userRepository = userRepository;
            _jwtService = jwtService;
        }

        public async Task<string> CreateRecoveryTokenAsync(string email, CancellationToken cancellationToken)
        {
            try
            {
                UserEntity user = await _userRepository.GetUserByEmailAsync(email, cancellationToken);
                if (user == null)
                {
                    throw new Exception();
                }

                string token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(16));
                user.RecoveryTokenHash = _hashHelper.HashPassword(token);
                user.RecoveryTokenLifeTime = DateTime.UtcNow.AddMinutes(5);
                await _userRepository.SaveChanges(cancellationToken);

                return token;
            }
            catch (OperationCanceledException oex)
            {
                throw new Exception("User Sevice: CreateRecoveryTokenAsync operation were canceled");
            }
            catch (Exception ex)
            {
                throw new Exception("User Service: Problem with CreateRecoveryTokenAsync");
            }
        }

        public async Task<string> LoginWithPasswordAsync(UserLoginDto loginDto, CancellationToken cancellationToken)
        {
            try
            {
                UserEntity user = await _userRepository.GetUserByEmailAsync(loginDto.UserEmail.Trim(), cancellationToken);
                if (user == null)
                {
                    throw new UnauthorizedAccessException();
                }
                else if (!_hashHelper.Check(loginDto.Password, user.PasswordHash))
                {
                    throw new UnauthorizedAccessException();
                }

                return _jwtService.GenerateAccessToken(loginDto, user.Role.ToString());
            }
            catch (UnauthorizedAccessException uex)
            {
                throw new UnauthorizedAccessException("Incorrect email or password");
            }
            catch (OperationCanceledException oex)
            {
                throw new Exception("User Sevice: LoginWithPasswordAsync operation were canceled");
            }
            catch (Exception ex)
            {
                throw new Exception("User Service: Problem with LoginWithPasswordAsync");
            }
        }

        public async Task<string> LoginWithRecoveryTokenAsync(UserLoginDto loginDto, CancellationToken cancellationToken)
        {
            try
            {
                UserEntity user = await _userRepository.GetUserByEmailAsync(loginDto.UserEmail.Trim(), cancellationToken);
                if (user == null)
                {
                    throw new UnauthorizedAccessException();
                }
                else if (user.RecoveryTokenLifeTime >= DateTime.Now)
                {
                    throw new UnauthorizedAccessException();
                }
                else if (!_hashHelper.Check(loginDto.Password, user.RecoveryTokenHash))
                {
                    throw new UnauthorizedAccessException();
                }

                user.RecoveryTokenHash = null;
                user.RecoveryTokenLifeTime = null;

                return _jwtService.GenerateAccessToken(loginDto, user.Role.ToString());
            }
            catch (UnauthorizedAccessException uex)
            {
                throw new UnauthorizedAccessException("Invalid email or token, or token expired");
            }
            catch (OperationCanceledException oex)
            {
                throw new Exception("User Sevice: LoginWithRecoveryTokenAsync operation were canceled");
            }
            catch (Exception ex)
            {
                throw new Exception("User Service: Problem with LoginWithRecoveryTokenAsync");
            }
        }

        public async Task<int> CreateUserAsync(UserPostDto userDto, CancellationToken cancellationToken)
        {
            try
            {
                UserEntity user = _mapper.Map<UserEntity>(userDto);
                return await _userRepository.CreateUserAsync(user, userDto.Password, cancellationToken);
            }
            catch (OperationCanceledException oex)
            {
                throw new Exception("User Service: CreateUserAsync operation were canceled");
            }
            catch (Exception ex)
            {
                throw new Exception("User Service: Problem with CreateUserAsync");
            }
        }

        public async Task<ICollection<UserGetDto>> GetAllUsersAsync(CancellationToken cancellationToken)
        {
            try
            {
                ICollection<UserEntity> users = await _userRepository.GetAllUsersAsync(cancellationToken);
                return _mapper.Map<ICollection<UserGetDto>>(users);
            }
            catch (OperationCanceledException oex)
            {
                throw new Exception("User Service: GetAllUsersAsync operation were canceled");
            }
            catch (Exception ex)
            {
                throw new Exception("User Service: Problem with GetAllUsersAsync");
            }
        }

        public async Task<UserGetDto> GetUserByEmailAsync(string email, CancellationToken cancellationToken)
        {
            try
            {
                UserEntity user = await _userRepository.GetUserByEmailAsync(email, cancellationToken);
                return _mapper.Map<UserGetDto>(user);
            }
            catch (OperationCanceledException oex)
            {
                throw new Exception("User Sevice: GetUserByEmailAsync operation were canceled");
            }
            catch (Exception ex)
            {
                throw new Exception("User Service: Problem with GetUserByEmailAsync");
            }
        }

        public async Task<UserGetDto> UpdateUserAsync(UserUpdateDto newUser, CancellationToken cancellationToken)
        {
            try
            {
                UserEntity user = _mapper.Map<UserEntity>(newUser);
                user = await _userRepository.UpdateUserAsync(user, newUser.Password, cancellationToken);
                return _mapper.Map<UserGetDto>(user);
            }
            catch (OperationCanceledException oex)
            {
                throw new Exception("User Service: UpdateUserAsync operation were canceled");
            }
            catch (Exception ex)
            {
                throw new Exception("User Service: Problem with UpdateUserAsync");
            }
        }
    }
}
