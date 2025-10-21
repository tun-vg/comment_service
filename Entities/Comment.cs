using System.ComponentModel.DataAnnotations;

namespace comment_service.Entities;

public class Comment
{
    [Key]
    public Guid CommentId { get; set; }
    
    public string Content { get; set; } = string.Empty;
    
    public DateTime CreatedAt { get; set; }
    
    public DateTime UpdatedAt { get; set; }

    public Guid PostId { get; set; }

    public Guid UserId { get; set; }
}
