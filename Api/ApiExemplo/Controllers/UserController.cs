using ApiExemplo.Dto;
using ApiExemplo.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApiExemplo.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpPost]
    public async Task<IActionResult> AddUser(UserDto user)
    {
        var userAdded = await _userRepository.Add(user);
        return Ok(userAdded);
    }

    [HttpPut("{userId}")]
    public async Task<IActionResult> UpdateUser([FromRoute] Guid userId, UserDto user)
    {
        var userUpdated = await _userRepository.Update(user, userId);
        return Ok(userUpdated);
    }

    [HttpDelete("{userId}")]
    public async Task<IActionResult> DeleteUser([FromRoute] Guid userId)
    {
        var userDeleted = await _userRepository.Delete(userId);
        return Ok(userDeleted);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _userRepository.GetAll();
        return Ok(users);
    }
}
