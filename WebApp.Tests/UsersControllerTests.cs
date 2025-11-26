namespace WebApp.Tests;
using System.Threading.Tasks;
using Xunit;
using Applications.Dtos;
using Applications.Interfaces;
using Domains.ValueObjects;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebApplication3.WebRequests;
using Web.Controllers; 
public class UsersControllerTests
{
    private Mock<IUserService> _userServiceMock = new();
    private Mock<IJWTService> _jwtServiceMock = new();
    private readonly UsersController _controller;

    public UsersControllerTests()
    { 
        _controller = new UsersController(_userServiceMock.Object, _jwtServiceMock.Object);
    }

    [Fact]
    public async Task Login_Returns_Unauthorized_Result_When_User_Not_Found()
    {
        var request = new LoginRequest{Username = "test", Password = "test"};
        _userServiceMock.Setup(s => s.Authentication(It.IsAny<Username>(), request.Password)).ReturnsAsync((UserDto?)null);
        var result = await _controller.Login(request);
        Assert.IsType<UnauthorizedObjectResult>(result);
    }

    [Fact]
    public async Task Login_Returns_Ok_Result_When_User_Found()
    {
        var request = new LoginRequest{Username = "test", Password = "test"};
        var UserDto = new UserDto(Guid.NewGuid(), new Username(request.Username));
        _userServiceMock.Setup(s => s.Authentication(It.IsAny<Username>(), request.Password)).ReturnsAsync(UserDto);
        _jwtServiceMock.Setup(s => s.GenerateJWTToken(UserDto)).Returns("jwtToken");
        var result = await _controller.Login(request);
        var ok = Assert.IsType<OkObjectResult>(result);
        dynamic body = ok.Value!;
        Assert.Equal("jwtToken", (string)body.token);
        Assert.Equal(UserDto.Id, body.user.id);
    }
}