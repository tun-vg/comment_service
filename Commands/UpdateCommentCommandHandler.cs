using comment_service.Common.Interfaces;
using comment_service.Entities;

namespace comment_service.Commands;

public class UpdateCommentCommandHandler : ICommandHandler<UpdateCommentCommand, Comment>
{
    private readonly ApplicationDBContext _context;

    public UpdateCommentCommandHandler(ApplicationDBContext context)
    {
        _context = context;
    }

    public async Task<Comment> Handle(UpdateCommentCommand command, CancellationToken cancellationToken)
    {
        var comment = await _context.Comments.FindAsync(command.CommentId, cancellationToken);
        if (comment != null)
        {
            comment.Content = command.Content;
            comment.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();
            return comment;
        }
        else
        {
            throw new Exception("Comment not found");
        }
    }
}