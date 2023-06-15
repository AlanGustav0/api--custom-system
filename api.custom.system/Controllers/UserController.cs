
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using api__custom_system.Repository.Dto;
using api__custom_system.Models;
using api.custom.system.Service.Interfaces;
using api.custom.system.Repository.Dto;
using api.custom.system.Models;

namespace api__custom_system.Controllers;

[ApiController]
[Route("usuario")]
public class UserController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IUserService _userService;

    
    public UserController(IMapper mapper, IUserService userService)
    {
        _mapper = mapper;
        _userService = userService;
    }

    [HttpPost]
    [Route("cadastrar")]
    public IActionResult CreateUser([FromBody] UserRequestDto userDto)
    {
 
        var user = _userService.CreateUser(userDto);

        return CreatedAtAction(nameof(GetUserById), new { user.Id }, user);
      
    }

    [HttpPost("cadastrar/imagem")]
    public IActionResult SaveImageProfileById([FromForm] ICollection<IFormFile> file, int id)
    {
        _userService.SaveImageProfile(file, id);
        return Ok();

    }

    [HttpGet]
    public IActionResult GetUserById([FromQuery] int id)
    {
        User user = _userService.GetUserById(id);
        if(user != null)
        {
            UserResponseDto userCreated = _mapper.Map<UserResponseDto>(user);
            return Ok(userCreated);
        }
        return NotFound();
    }

    [HttpGet("perfil")]
    public IActionResult GetUserProfileById([FromQuery] int id)
    {
        UserProfile userProfile = _userService.GetUserProfile(id);
        if (userProfile != null)
        {
            UserProfileResponseDto userProfileCreated = _mapper.Map<UserProfileResponseDto>(userProfile);
            return Ok(userProfileCreated);
        }
        return NotFound();
    }

    [HttpPut("atualizar/perfil")]
    public IActionResult UpdateUserProfile([FromBody] UserProfileRequestDto userProfileDto)
    {
        
        var profile = _userService.UpdateUserProfile(userProfileDto);

        if (profile != null)
        {
            return Ok();
        }

        return BadRequest();

    }
}
