using comment_service.Common.Interfaces;
using comment_service.Entities;
using Microsoft.EntityFrameworkCore;

namespace comment_service.Queries;

public class GetCommentsByPostIdQueryHandler : IQueryHandler<GetCommentsByPostIdQuery, List<Comment>>
{
    private readonly ApplicationDBContext _context;
    
    public GetCommentsByPostIdQueryHandler(ApplicationDBContext context)
    {
        _context = context;
    }

    public async Task<List<Comment>> Handle(GetCommentsByPostIdQuery query, CancellationToken cancellationToken)
    {
        var  comments = await _context.Comments.Where(c => c.PostId == query.PostId).ToListAsync(cancellationToken);
        return comments;
    }
}