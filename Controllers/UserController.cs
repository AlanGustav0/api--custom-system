
using Microsoft.AspNetCore.Mvc;
using api__custom_system.Repository;
using AutoMapper;
using api__custom_system.Repository.Dto;
using api__custom_system.Models;

namespace api__custom_system.Controllers;

[ApiController]
[Route("usuario")]
public class UserController : ControllerBase
{
    private readonly DatabaseContext _context;
    private readonly IMapper _mapper;

    
    public UserController(DatabaseContext context,IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpPost]
    [Route("cadastrar")]
    public IActionResult RegisterUser([FromBody] UserRequestDto userDto)
    {
        User user = _mapper.Map<User>(userDto);
        _context.Add(user);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetUserById), new {user.Id},user);
    }

    [HttpGet("{id}")]
    public IActionResult GetUserById(int id)
    {
        User user = _context.UserInfos.FirstOrDefault(value => value.Id == id);
        if(user != null)
        {
            UserResponseDto userCreated = _mapper.Map<UserResponseDto>(user);
            return Ok(userCreated);
        }
        return NotFound();
    }

  
}
