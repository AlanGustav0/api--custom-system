using api__custom_system.Models;
using api__custom_system.Repository;
using api__custom_system.Repository.Dto;
using api__custom_system.Service;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api__custom_system.Controllers
{
    [ApiController]
    [Route("login")]
    public class LoginController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public LoginController(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<ActionResult<TokenResponse>> AuthenticationAsync([FromBody] UserRequestDto userDto)
        {
            var user = _context.UserInfos.FirstOrDefault(value => value.UserName == userDto.UserName && value.Password == userDto.Password);

            if(user == null)
            {
                return Unauthorized(user);   
            }
            UserResponseDto userResponse = _mapper.Map<UserResponseDto>(user);

            var token = TokenService.GenerateToken(userResponse);

            return new TokenResponse
            {
                User = userResponse,
                Token = token
            };
        }
    }
}
