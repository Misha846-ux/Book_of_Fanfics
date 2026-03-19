using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FanfictionBook.Application.DTOs.UserDTOs;
using FanfictionBook.Domain.Entities;

namespace FanfictionBook.Application.Mapping
{
    public class UserProfile: Profile
    {
        public UserProfile()
        {
            CreateMap<UserPostDto, UserEntity>();
            CreateMap<UserUpdateDto, UserEntity>();
            CreateMap<UserEntity, UserGetDto>()
                .ForMember(dest => dest.Fanfics, opt => opt
                .MapFrom(dest => dest.Fanfics.Select(f => f.Id)));

        }
    }
}
