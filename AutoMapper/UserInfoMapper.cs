using api__auth.Models;
using api__auth.Repository.Dto;
using AutoMapper;

namespace api__auth.AutoMapper
{
    public class UserInfoMapper : Profile
    {
        public UserInfoMapper() 
        {
            CreateMap<UserInfoDto, UserInfo>();
        }
    }
}
