using Microsoft.EntityFrameworkCore;
using Pet_Get.Data;
using Pet_Get.Interface;
using Pet_Get.Models;
using Pet_Get.Models.DTOs;

namespace Pet_Get.Repo;

public class ForumRepo : IForumRepo
{
    private readonly ApplicationDbContext _context;

    public ForumRepo(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Forum>> GetAllForumsAsync()
    {
        return await _context.Forums.Include(f => f.Comments).ToListAsync();
    }

    public async Task<Forum> GetForumByIdAsync(int id)
    {
        return await _context.Forums.Where(f => f.Id == id).Include(f => f.Comments).FirstOrDefaultAsync();
    }

    public async Task AddForumAsync(AddForumDTO forum)
    {
        var newForum = new Forum
        {
            Title = forum.Title,
            Description = forum.Description,
            createdAt = DateTime.Now,
        };
        
        
        if (forum.Image != null && forum.Image.Length > 0)
        {
            using (var memoryStream = new MemoryStream())
            {
                await forum.Image.CopyToAsync(memoryStream);
                string base64String = Convert.ToBase64String(memoryStream.ToArray());
                string prefixedBase64String = $"data:image/jpeg;base64,{base64String}";

                newForum.Image = prefixedBase64String;
            }
        }
        
        _context.Forums.Add(newForum);
        await _context.SaveChangesAsync();
    }
}