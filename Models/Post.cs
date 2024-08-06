using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Pet_Get.Models;

public class Post
{
    public int Id { get; set; }

    public string? UserId { get; set; }
    [ForeignKey(nameof(UserId))] 
    public AppUser? AppUser { get; set; }

    public string Title { get; set; }
    public string Description { get; set; }
    
    public string Image { get; set; }

    public string Email { get; set; }
    public bool Featured { get; set; } = false;

    public int AnimalTypeId { get; set; }
    [ForeignKey(nameof(AnimalTypeId))] 
    public AnimalType AnimalType { get; set; }

    public bool Approved { get; set; }

    public string PhoneNumber { get; set; }
    
    public DateTime createdAt { get; set; }
    public DateTime? editedAt { get; set; }

    public List<Bookmark> Bookmarks { get; set; }

}