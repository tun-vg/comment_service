using comment_service.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace comment_service.Commands;

public class DeleteCommentCommandHandler : ICommandHandler<DeleteCommentCommand, bool>
{
    private readonly ApplicationDBContext _context;
    
    public DeleteCommentCommandHandler(ApplicationDBContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteCommentCommand command, CancellationToken cancellationToken)
    {
        var comment = await _context.Comments.Where(x => x.CommentId == command.CommentId)
            .FirstOrDefaultAsync(cancellationToken);

        if (comment != null)
        {
            _context.Comments.Remove(comment);
            var rowChanged = await _context.SaveChangesAsync(cancellationToken);
            return rowChanged == 1;
        }
        else throw new Exception("Comment not found");
    }
}