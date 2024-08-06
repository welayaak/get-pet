namespace Pet_Get.Models.DTOs;

public class CreatePostDTO
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Email { get; set; }
    public IFormFile? Image { get; set; }
    public int AnimalTypeId { get; set; }
    public string? NewAnimalType { get; set; }
    public string PhoneNumber { get; set; }
    public bool Featured { get; set; }
    public bool Approved { get; set; }

    public List<AnimalType> AnimalTypes { get; set; }
    public Post Post { get; set; }
}