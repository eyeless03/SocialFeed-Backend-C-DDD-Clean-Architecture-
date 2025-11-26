using Domains.ValueObjects;
using Microsoft.AspNetCore.Mvc;
using WebApplication3.WebRequests;
using Applications.Interfaces;

namespace Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IJWTService _jwtService;
    public UsersController(IUserService userService, IJWTService jwtService)
    {
        _userService = userService;
        _jwtService = jwtService;
    }
    [HttpPost]
    public async Task<IActionResult> Register([FromBody] CreateUserRequest request)
    {
        var username = new Username(request.Username);
        var userId = await _userService.CreateUserAsync(username, request.Password);
        return Ok(new
        {
            Id = userId.Value,
            Username = username.Value
        });
    }
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var username = new Username(request.Username);
        var userDto = await _userService.Authentication(username, request.Password);
        if (userDto is null)
        {
            return Unauthorized("Invalid username or password");
        }
        var token = _jwtService.GenerateJWTToken(userDto);
        return Ok(new {token, user = new { id = userDto.Id, username = userDto.Username }});
    }
}