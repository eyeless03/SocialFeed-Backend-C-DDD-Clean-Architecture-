namespace WebApp.Tests;
using Xunit;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Applications.Dtos;
using Applications.Interfaces;
using Domains.ValueObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Web.Controllers;
using WebApplication3.WebRequests;
public class PostsControllerTests
{
    private readonly Mock<IPostService> _postServiceMock = new();
    private readonly PostsController _controller;

    public PostsControllerTests()
    {
        _controller = new PostsController(_postServiceMock.Object);
    }

    public void SetUser(Guid authorId, string username = "123")
    {
        var identity = new ClaimsIdentity(new []
        {
            new Claim(ClaimTypes.NameIdentifier, authorId.ToString()),
            new Claim(ClaimTypes.Name, username.ToString())
        });
        var principal = new ClaimsPrincipal(identity);
        _controller.ControllerContext.HttpContext = new DefaultHttpContext { User = principal };
        
    }
    [Fact]
    public async Task AddComment_Returns_Unauthorized_When_User_Not_Found()
    {
        var postId = Guid.NewGuid();
        var request = new CreateCommentRequest{Content = "test"};
        var result = await _controller.AddCommentToPostAsync(postId, request);
        var unauthorized = Assert.IsType<UnauthorizedObjectResult>(result);
        Assert.Equal("User not found", unauthorized.Value);

        _postServiceMock.Verify(
            s => s.AddCommentAsync(
                It.IsAny<PostId>(),
                It.IsAny<UserId>(),
                It.IsAny<CommentText>()),
            Times.Never);
    }
    [Fact]
    public async Task AddComment_Returns_Ok_With_CommentId()
    {
        var postId = Guid.NewGuid();
        var request = new CreateCommentRequest { Content = "nice post" };
        var userId = Guid.NewGuid();
        var commentId = CommentId.From(Guid.NewGuid());
        SetUser(userId);
        _postServiceMock
            .Setup(s => s.AddCommentAsync(
                It.Is<PostId>(p => p.Value == postId),
                It.Is<UserId>(u => u.Value == userId),
                It.IsAny<CommentText>()))
            .ReturnsAsync(commentId);
        var result = await _controller.AddCommentToPostAsync(postId, request);
        var ok = Assert.IsType<OkObjectResult>(result);
        dynamic body = ok.Value!;
        Assert.Equal(commentId.Value, (Guid)body.id);
        _postServiceMock.Verify(
            s => s.AddCommentAsync(
                It.Is<PostId>(p => p.Value == postId),
                It.Is<UserId>(u => u.Value == userId),
                It.Is<CommentText>(t => t.Value == request.Content)),
            Times.Once);
    }
    [Fact]
    public async Task AddLike_Returns_Unauthorized_When_User_Not_Found()
    {
        var postId = Guid.NewGuid();
        
        var result = await _controller.AddLikeToPostAsync(postId);
        
        var unauthorized = Assert.IsType<UnauthorizedObjectResult>(result);
        Assert.Equal("User not found", unauthorized.Value);

        _postServiceMock.Verify(
            s => s.AddLikeOnPostAsync(It.IsAny<PostId>(), It.IsAny<UserId>()),
            Times.Never);
    }
    [Fact]
    public async Task AddLike_Returns_Ok_And_Calls_Service()
    {

        var postId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        SetUser(userId);

        _postServiceMock
            .Setup(s => s.AddLikeOnPostAsync(
                It.IsAny<PostId>(),
                It.IsAny<UserId>()))
            .Returns(Task.CompletedTask);


        var result = await _controller.AddLikeToPostAsync(postId);


        Assert.IsType<OkResult>(result);

        _postServiceMock.Verify(
            s => s.AddLikeOnPostAsync(
                It.Is<PostId>(p => p.Value == postId),
                It.Is<UserId>(u => u.Value == userId)),
            Times.Once);
    }
}