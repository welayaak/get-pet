using Microsoft.EntityFrameworkCore;
using Pet_Get.Data;
using Pet_Get.Interface;
using Pet_Get.Models;

namespace Pet_Get.Repo;

public class CommentRepo : ICommentRepo
{
    private readonly ApplicationDbContext _context;

    public CommentRepo(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task AddCommentAsync(Comment comment)
    {
        _context.Comments.Add(comment);
        await _context.SaveChangesAsync();
    }
    
    public async Task<List<Comment>> GetCommentsByForumIdAsync(int forumId)
    {
        return await _context.Comments.Where(c => c.ForumId == forumId).ToListAsync();
    }
}