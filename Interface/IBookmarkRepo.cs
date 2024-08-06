using Pet_Get.Models;

namespace Pet_Get.Interface;

public interface IBookmarkRepo
{
    Task<List<Bookmark>> GetAllBookmarksAsync();
    Task AddBookmarkAsync(string userId, int postId);
    Task DeleteBookmarkAsync(string userId, int postId);
}