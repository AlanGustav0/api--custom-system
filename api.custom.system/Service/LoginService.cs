using api.custom.system.Repository.Interfaces;
using api__custom_system.Models;
using api__custom_system.Repository.Dto;
using api__custom_system.Service.Interfaces;
using AutoMapper;

namespace api__custom_system.Service
{
    public class LoginService : ILoginService
    {
        private readonly ILoginRepository _loginRepository;
        private readonly IMapper _mapper;


        public LoginService(ILoginRepository loginRepository, IMapper mapper)
        {
            _loginRepository = loginRepository;
            _mapper = mapper;
        }
        public async Task<UserResponseDto> AuthUser(string userName, string password)
        {
            User? user = await _loginRepository.AuthUser(userName, password);

            UserResponseDto userResponse = _mapper.Map<UserResponseDto>(user);

            return userResponse;
        }
    }
}
