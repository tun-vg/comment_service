using comment_service.Common.Interfaces;
using comment_service.Entities;

namespace comment_service.Application.Commands;

public class CreateCommentCommandHandler : ICommandHandler<CreateCommentCommand, Comment>
{
    private readonly ApplicationDBContext _context;
    private readonly ICacheVersionManagement _cacheVersionManagement;
    
    public CreateCommentCommandHandler(ApplicationDBContext context, ICacheVersionManagement cacheVersionManagement)
    {
        _context = context;
        _cacheVersionManagement = cacheVersionManagement;
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

        if (command.UpperCommentId == null)
        {
            await _cacheVersionManagement.BumpCacheVersionAsync($"GetCommentsByPostId:Post={command.PostId}");
        } else
        {
            await _cacheVersionManagement.BumpCacheVersionAsync($"GetCommentRepliesByCommentId:Comment={command.UpperCommentId}");
        }

        return comment;
    }
}