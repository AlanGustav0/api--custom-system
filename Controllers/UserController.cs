
using Microsoft.AspNetCore.Mvc;
using api__auth.Repository;
using AutoMapper;
using api__auth.Repository.Dto;
using api__auth.Models;

namespace api__auth.Controllers;

[ApiController]
[Route("[controller]")]
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
    public IActionResult CadastrarUsuario([FromBody] UserInfoDto userDto)
    {
        UserInfo user = _mapper.Map<UserInfo>(userDto);
        _context.Add(user);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetUserById), new {user.Id},user);
    }

    [HttpGet]
    public IActionResult GetUserById(int id)
    {
        UserInfo user = _context.UserInfos.FirstOrDefault(value => value.Id == id);
        if(user != null)
        {
            UserInfoCreatedDto userCreated = _mapper.Map<UserInfoCreatedDto>(user);
            return Ok(userCreated);
        }
        return NotFound();
    }

  
}
