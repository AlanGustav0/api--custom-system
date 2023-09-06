
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using api__custom_system.Repository.Dto;
using api__custom_system.Models;
using api.custom.system.Service.Interfaces;
using api.custom.system.Repository.Dto;
using api.custom.system.Models;

namespace api__custom_system.Controllers;

[ApiController]
[Route("api/v1/usuario")]
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
    public async Task<IActionResult> CreateUser([FromBody] UserRequestDto userDto)
    {
        try
        {
            var user = await _userService.CreateUser(userDto);

            return CreatedAtAction(nameof(GetUserById), new { user.Id }, user);
        }
        catch
        (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
        
      
    }

    [HttpPost("cadastrar/imagem")]
    public async Task<IActionResult> SaveImageProfileById([FromForm] ProfileData profileData)
    {
        try
        {
            await _userService.SaveImageProfile(profileData);
        }catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }

        return Ok();

    }

    [HttpGet]
    public async Task<IActionResult> GetUserById([FromQuery] int id)
    {
        try
        {
            User? user = await _userService.GetUserById(id);
            if (user != null)
            {
                UserResponseDto userCreated = _mapper.Map<UserResponseDto>(user);
                return Ok(userCreated);
            }
            return NotFound();

        }
        catch(Exception ex) 
        {
            Console.WriteLine(ex.Message);
            throw;
        }
        
    }

    [HttpGet("perfil")]
    public async Task<IActionResult> GetUserProfileById([FromQuery] int id)
    {
        try
        {
            UserProfile userProfile = await _userService.GetUserProfile(id);
            if (userProfile != null)
            {
                UserProfileResponseDto userProfileCreated = _mapper.Map<UserProfileResponseDto>(userProfile);
                return Ok(userProfileCreated);
            }
            return NotFound();
        }
        catch(Exception ex) 
        {
            Console.WriteLine(ex.Message);
            throw;
        }
        
    }

    [HttpPut("atualizar/perfil")]
    public async Task<IActionResult> UpdateUserProfile([FromBody] UserProfileRequestDto userProfileDto)
    {
        try
        {
            var profile = await _userService.UpdateUserProfile(userProfileDto);

            if (profile != null)
            {
                return Ok();
            }

            return BadRequest();
        }
        catch
        (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
        

    }
}
