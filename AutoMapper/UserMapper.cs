using api__auth.Models;
using api__auth.Repository.Dto;
using AutoMapper;

namespace api__auth.AutoMapper
{
    public class UserMapper : Profile
    {
        public UserMapper() 
        {
            CreateMap<UserRequestDto, User>();
            CreateMap<User, UserResponseDto>();
        }
    }
}
