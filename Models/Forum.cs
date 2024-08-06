using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pet_Get.Models;

public class Forum
{
    public int Id { get; set; }

    public string Title { get; set; }
    public string Description { get; set; }

    public DateTime createdAt { get; set; }

    public string Image { get; set; }

    public List<Comment> Comments { get; set; }
}