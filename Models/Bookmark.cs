using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pet_Get.Models;

public class Bookmark
{
    public int Id { get; set; }

    [Required]
    public string UserId { get; set; }
    [ForeignKey(nameof(UserId))] 
    public AppUser AppUser { get; set; }

    [Required]
    public int PostId { get; set; }
    [ForeignKey(nameof(PostId))] 
    public Post Post { get; set; }
}