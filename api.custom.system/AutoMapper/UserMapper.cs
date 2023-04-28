using api__custom_system.Models;
using api__custom_system.Repository.Dto;
using AutoMapper;

namespace api__custom_system.AutoMapper
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
