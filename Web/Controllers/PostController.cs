using Domains.ValueObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication3.WebRequests;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Web.Controllers;
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class PostsController : ControllerBase
{
    private readonly IPostService _postService;

    public PostsController(IPostService postService)
    {
        _postService = postService;
    }

    [HttpPost]
    [HttpPost("create")]
    public async Task<IActionResult> CreatePostAsync([FromBody] CreatePostRequest request)
    {
        var title = new PostText(request.Title);
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
        {
            return Unauthorized("User not found");
        }

        var authorId = UserId.From(Guid.Parse(userIdClaim.Value));
        var id = await _postService.CreatePostAsync(title, authorId);
        return Ok(new 
        {
            AuthorId = authorId.Value,
            Title = title.Value,
            Id = id.Value
        });
    }


    [HttpPost("{postId}/like")]
    public async Task<IActionResult> AddLikeToPostAsync(Guid postId)
    {
        var userIdClaim = User.FindFirst(JwtRegisteredClaimNames.Sub);
        if (userIdClaim == null)
        {
            return Unauthorized("User not found");
        }
        var postIdVO = PostId.From(postId);
        var userIdVO = UserId.From(Guid.Parse(userIdClaim.Value));
        await _postService.AddLikeOnPostAsync(postIdVO, userIdVO);
        
        return Ok();
    }

    [HttpDelete("{postId}/like")]
    public async Task<IActionResult> RemoveLikeFromPostAsync(Guid postId)
    {
        var userIdClaim = User.FindFirst(JwtRegisteredClaimNames.Sub);
        if (userIdClaim == null)
        {
            return Unauthorized("User not found");
        }
        var postIdVO = PostId.From(postId);
        var userIdVO = UserId.From(Guid.Parse(userIdClaim.Value));
        await _postService.RemoveLikeFromPostAsync(postIdVO, userIdVO);
        return NoContent();
    }
    [HttpGet("me")]
    public IActionResult Me()
    {
        return Ok(new
        {
            IsAuthenticated = User.Identity?.IsAuthenticated,
            Name = User.Identity?.Name,
            Claims = User.Claims.Select(c => new { c.Type, c.Value }).ToList()
        });
    }

    [HttpPost("{postId}/dislike")]
    public async Task<IActionResult> AddDislikeOnPostAsync(Guid postId)
    {
        var userIdClaim = User.FindFirst(JwtRegisteredClaimNames.Sub);
        if (userIdClaim == null)
        {
            return Unauthorized("User not found");
        }
        var postIdVO = PostId.From(postId);
        var userIdVO = UserId.From(Guid.Parse(userIdClaim.Value));
        await _postService.AddDislikeOnPostAsync(postIdVO,userIdVO);
        return Ok();
    }

    [HttpDelete("{postId}/dislike")]
    public async Task<IActionResult> RemoveDislikeFromPostAsync(Guid postId)
    {
        var userIdClaim = User.FindFirst(JwtRegisteredClaimNames.Sub);
        if (userIdClaim == null)
        {
            return  Unauthorized("User not found");
        }
        var postIdVO = PostId.From(postId);
        var userIdVO = UserId.From(Guid.Parse(userIdClaim.Value));
        await _postService.RemoveDislikeFromPostAsync(postIdVO, userIdVO);
        return NoContent();
    }

    [HttpPost("{postId}/comments")]
    public async Task<IActionResult> AddCommentToPostAsync(Guid postId, [FromBody] CreateCommentRequest request)
    {
        var userIdClaim =User.FindFirst(JwtRegisteredClaimNames.Sub);
        if (userIdClaim == null)
        {
            return  Unauthorized("User not found");
        }
        var text = new CommentText(request.Content);
        var postIdVO = PostId.From(postId);
        var userId = UserId.From(Guid.Parse(userIdClaim.Value));
        var commentId = await _postService.AddCommentAsync(postIdVO, userId, text);
        return Ok(new
        {
            CommentId = commentId.Value,
            Text = text.Value,
            PostId = postIdVO.Value,
            UserId = userId.Value
        });
    }

    [HttpPost("{postId}/comments/{commentId}/likes")]
    public async Task<IActionResult> AddLikeOnCommentAsync(Guid postId, Guid commentId)
    {
        var userIdClaim = User.FindFirst(JwtRegisteredClaimNames.Sub);
        if (userIdClaim == null)
        {
            return Unauthorized();
        }
        var commentIdVO = CommentId.From(commentId);
        var postIdVO = PostId.From(postId);
        var userIdVO = UserId.From(Guid.Parse(userIdClaim.Value));
        await _postService.AddLikeOnCommentAsync(postIdVO,commentIdVO,userIdVO);
        return Ok();
    }

    [HttpDelete("{postId}/comments/{commentId}/likes")]
    public async Task<IActionResult> RemoveLikeFromCommentAsync(Guid postId, Guid commentId)
    {
        var userIdClaim = User.FindFirst(JwtRegisteredClaimNames.Sub);
        if (userIdClaim == null)
        {
            return Unauthorized();
        }
        var postIdVO = PostId.From(postId);
        var commentIdVO = CommentId.From(commentId);
        var userIdVO = UserId.From(Guid.Parse(userIdClaim.Value));
        await _postService.RemoveLikeFromCommentAsync(postIdVO,commentIdVO,userIdVO);
        return NoContent();
    }

    [HttpPost("{postId}/comments/{commentId}/dislikes")]
    public async Task<IActionResult> AddDislikeOnCommentAsync(Guid postId, Guid commentId)
    {
        var userIdClaim = User.FindFirst(JwtRegisteredClaimNames.Sub);
        if (userIdClaim == null)
        {
            return Unauthorized();
        }
        var userIdVO = UserId.From(Guid.Parse(userIdClaim.Value));
        var postIdVO = PostId.From(postId);
        var commentIdVO = CommentId.From(commentId);
        await _postService.AddDislikeOnCommentAsync(postIdVO,commentIdVO,userIdVO);
        return Ok();
    }

    [HttpDelete("{postId}/comments/{commentId}/dislikes")]
    public async Task<IActionResult> RemoveDislikeOnCommentAsync(Guid postId, Guid commentId)
    {
        var userIdClaim = User.FindFirst(JwtRegisteredClaimNames.Sub);
        if (userIdClaim == null)
        {
            return Unauthorized();
        }
        var userIdVO = UserId.From(Guid.Parse(userIdClaim.Value));
        var postIdVO = PostId.From(postId);
        var commentIdVO = CommentId.From(commentId);
        await _postService.RemoveDislikeFromCommentAsync(postIdVO, commentIdVO,userIdVO);
        return NoContent();
    }
}