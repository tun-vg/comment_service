using comment_service.Common.Interfaces;
using comment_service.Entities;

namespace comment_service.Commands;

public class CreateCommentCommandHandler(ApplicationDBContext context) : ICommandHandler<CreateCommentCommand, Comment>
{
    public async Task<Comment> Handle(CreateCommentCommand command, CancellationToken cancellationToken)
    {
        Comment comment = new Comment();
        await context.Comments.AddAsync(comment, cancellationToken);
        return comment;
    }
}