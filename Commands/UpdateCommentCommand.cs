using comment_service.Common.Interfaces;
using comment_service.Entities;

namespace comment_service.Commands;

public class UpdateCommentCommand : ICommand<Comment>
{
    public Guid CommentId { get; set; }
    
    public string Content { get; set; }
}