using api__custom_system.Models;
using api__custom_system.Repository;
using api__custom_system.Repository.Dto;
using api__custom_system.Service.Interfaces;
using AutoMapper;

namespace api__custom_system.Service
{
    public class LoginService : ILoginService
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;


        public LoginService(DatabaseContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public UserResponseDto AuthUser(string userName, string password)
        {
            User? user = _context.UserInfos?.FirstOrDefault(value => value.UserName == userName && value.Password == password);

            UserResponseDto userResponse = _mapper.Map<UserResponseDto>(user);

            return userResponse;
        }
    }
}
