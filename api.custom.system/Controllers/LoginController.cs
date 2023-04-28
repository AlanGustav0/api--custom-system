using api__custom_system.Models;
using api__custom_system.Repository.Dto;
using api__custom_system.Service;
using api__custom_system.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace api__custom_system.Controllers
{
    [ApiController]
    [Route("login")]
    public class LoginController : ControllerBase
    {
        private ILoginService _loginService;


        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }
        [HttpPost]
        public ActionResult<TokenResponse> Authentication([FromBody] UserRequestDto userDto)
        {

            try
            {
                var user = _loginService.AuthUser(userDto.UserName, userDto.Password);
                var token = TokenService.GenerateToken(user);

                return new TokenResponse
                {
                    User = user,
                    Token = token
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Unauthorized();
            }
        }
    }
}
