
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using api__custom_system.Repository.Dto;
using api__custom_system.Models;
using api.custom.system.Service.Interfaces;
using api.custom.system.Models;
using api.custom.system.Repository.Dto;

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
        User user = _mapper.Map<User>(userDto);

        var userProfileId = _userService.CreateUserProfile(user).Result;
        user.UserProfileId = userProfileId.Id;
        _userService.CreateUser(user);
        
        return CreatedAtAction(nameof(GetUserById), new {user.Id},user);
    }

    [HttpPost("cadastrar/imagem/perfil")]
    public IActionResult SaveImageProfileById([FromForm] ICollection<IFormFile> file, int id)
    {
        var uploadImage = _userService.SaveImageProfile(file, id);

        if (uploadImage != null)
        {
            return Ok();
        }

        return BadRequest();

    }

    [HttpGet("{id}")]
    public IActionResult GetUserById(int id)
    {
        User user = _userService.GetUserById(id).Result;
        if(user != null)
        {
            UserResponseDto userCreated = _mapper.Map<UserResponseDto>(user);
            return Ok(userCreated);
        }
        return NotFound();
    }

    [HttpPut("atualizar/perfil")]
    public IActionResult UpdateProfile([FromBody] UserProfileRequestDto userProfile)
    {
        var profile = _mapper.Map<UserProfile>(userProfile);
        _userService.UpdateUserProfile(profile);

        if (profile != null)
        {
            return Ok(profile);
        }

        return BadRequest();

    }



    [HttpGet("obter/imagem/perfil")]
    public IActionResult GetImageProfileById(int id)
    {
        var image = _userService.GetProfileById(id).Result;

        if (image != null)
        {
            return Ok(image.ImageProfile);
        }

        return BadRequest();

    }


}
