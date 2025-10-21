using comment_service.Common.Interfaces;
using comment_service.Entities;

namespace comment_service.Queries;

public class GetCommentsByPostIdQuery : IQuery<List<Comment>>
{
    public Guid PostId { get; set; }
    
    public GetCommentsByPostIdQuery(Guid postId)
    {
      PostId = postId;
    }
}