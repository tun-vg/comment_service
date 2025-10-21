using comment_service.Commands;
using comment_service.Dispatcher;
using comment_service.Queries;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace comment_service.Controllers;

[ApiController]
[Route("[controller]")]
public class CommentController : ControllerBase
{
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IQueryDispatcher _queryDispatcher;

    public CommentController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
    {
        _commandDispatcher = commandDispatcher;
        _queryDispatcher = queryDispatcher;
    }

    [HttpGet]
    public async Task<IActionResult> GetCommentByPostId(Guid postId)
    {
        GetCommentsByPostIdQuery query = new GetCommentsByPostIdQuery(postId);
        var result = await _queryDispatcher.Send(query);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateComment(CreateCommentCommand command)
    {
        var result = await _commandDispatcher.Send(command);
        return Created(string.Empty, result);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateComment(UpdateCommentCommand command)
    {
        var result = await _commandDispatcher.Send(command);
        return Ok(result);
    }

    [HttpDelete("{commentId}")]
    public async Task<IActionResult> DeleteComment(Guid commentId)
    {
        DeleteCommentCommand command = new DeleteCommentCommand(commentId);
        var result = await _commandDispatcher.Send(command);
        return Ok(result);
    }
}