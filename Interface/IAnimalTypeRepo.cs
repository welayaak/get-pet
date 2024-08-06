using Pet_Get.Models;

namespace Pet_Get.Interface;

public interface IAnimalTypeRepo
{
    Task<List<AnimalType>> GetAllPostsAsync();
    Task<AnimalType> AddAnimalTypeAsync(string type);
}