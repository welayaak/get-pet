namespace Pet_Get.Models.DTOs;

public class AddForumDTO
{
    public string Title { get; set; }
    public string Description { get; set; }
    public IFormFile Image { get; set; }
}