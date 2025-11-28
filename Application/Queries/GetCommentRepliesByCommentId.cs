using comment_service.Common.Interfaces;
using comment_service.Entities;

namespace comment_service.Application.Queries;

public class GetCommentRepliesByCommentId : IQuery<IEnumerable<Comment>>
{
    public Guid CommentId { get; set; }

    public GetCommentRepliesByCommentId(Guid commentId)
    {
        CommentId = commentId;
    }
}
