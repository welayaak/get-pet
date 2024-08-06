using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pet_Get.Models;

public class Comment
{
    public int Id { get; set; }

    public string Body { get; set; }
    
    [Required]
    public int ForumId { get; set; }
    [ForeignKey(nameof(ForumId))] 
    public Forum Forum { get; set; }
    
    public DateTime createdAt { get; set; }
    public DateTime? editedAt { get; set; }
}