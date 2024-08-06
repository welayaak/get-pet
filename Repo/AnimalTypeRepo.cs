using Microsoft.EntityFrameworkCore;
using Pet_Get.Data;
using Pet_Get.Interface;
using Pet_Get.Models;

namespace Pet_Get.Repo;

public class AnimalTypeRepo : IAnimalTypeRepo
{
    private readonly ApplicationDbContext _context;

    public AnimalTypeRepo(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<List<AnimalType>> GetAllPostsAsync()
    {
        return await _context.AnimalTypes.ToListAsync();
    }

    public async Task<AnimalType> AddAnimalTypeAsync(string type)
    {
        var newAnimalType = new AnimalType
        {
            Type = type
        };

        await _context.AnimalTypes.AddAsync(newAnimalType);
        await _context.SaveChangesAsync();

        return newAnimalType;
    }
}