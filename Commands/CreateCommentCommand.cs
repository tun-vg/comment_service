using comment_service.Common.Interfaces;
using comment_service.Entities;

namespace comment_service.Commands;

public class CreateCommentCommand : ICommand<Comment>
{
    public string Content { get; set; }
    public string AuthorId { get; set; }
    public string PostId { get; set; }
    public CreateCommentCommand(string content, string authorId, string postId)
    {
        Content = content;
        AuthorId = authorId;
        PostId = postId;
    }
}