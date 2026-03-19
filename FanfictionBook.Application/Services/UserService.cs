using System;
using System.Collections.Generic;
using System.Linq;
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
        public UserService(IMapper mapper,  IHashHelper hashHelper, IUserRepository userRepository)
        {
            _hashHelper = hashHelper;
            _mapper = mapper;
            _userRepository = userRepository;
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
