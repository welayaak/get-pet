using Microsoft.EntityFrameworkCore;
using Pet_Get.Data;
using Pet_Get.Interface;
using Pet_Get.Models;

namespace Pet_Get.Repo;

public class BookmarkRepo : IBookmarkRepo
{
    private readonly ApplicationDbContext _context;

    public BookmarkRepo(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Bookmark>> GetAllBookmarksAsync()
    {
        return await _context.Bookmarks.ToListAsync();
    }

    public async Task AddBookmarkAsync(string userId, int postId)
    {
        var bookmark = new Bookmark
        {
            UserId = userId,
            PostId = postId
        };

        _context.Bookmarks.Add(bookmark);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteBookmarkAsync(string userId, int postId)
    {
        var bookmarkToDelete =
            await _context.Bookmarks.FirstOrDefaultAsync(b => b.PostId == postId && b.UserId.Equals(userId));
        _context.Bookmarks.Remove(bookmarkToDelete);
        await _context.SaveChangesAsync();
    }
}