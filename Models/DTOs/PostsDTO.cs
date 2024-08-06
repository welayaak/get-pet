namespace Pet_Get.Models.DTOs;

public class PostsDTO
{
    public List<Post> Posts { get; set; }
    public List<AnimalType> AnimalTypes { get; set; }
    public string? search { get; set; } = "";
    public int? animalTypeId { get; set; } = 2;
}