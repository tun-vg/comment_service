using comment_service.Common.Interfaces;
using comment_service.Entities;

namespace comment_service.Application.Commands;

public class CreateCommentCommandHandler : ICommandHandler<CreateCommentCommand, Comment>
{
    private readonly ApplicationDBContext _context;
    
    public CreateCommentCommandHandler(ApplicationDBContext context)
    {
        _context = context;
    }

    public async Task<Comment> Handle(CreateCommentCommand command, CancellationToken cancellationToken)
    {
        Comment comment = new Comment
        {
            CommentId = Guid.NewGuid(),
            Content = command.Content,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            PostId = command.PostId,
            AuthorId = command.AuthorId,
            UpperCommentId = command.UpperCommentId
        };
        await _context.Comments.AddAsync(comment, cancellationToken);
        
        if (command.UpperCommentId != null)
        {
            var commentParent = await _context.Comments.FindAsync(command.UpperCommentId);
            if (commentParent != null)
            {
                commentParent.CommentReplyCount += 1;
            }
        }

        await _context.SaveChangesAsync(cancellationToken);

        return comment;
    }
}