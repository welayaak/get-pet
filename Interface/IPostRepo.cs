using Pet_Get.Models;
using Pet_Get.Models.DTOs;

namespace Pet_Get.Interface;

public interface IPostRepo
{
    Task<List<Post>> GetAllPostsAsync();
    Task<List<Post>> GetFilteredPostsAsync(string? search);
    Task<List<Post>> GetAllPendingPostsAsync();
    Task<Post> GetPostByIdAsync(int id);
    // Task AddPostAsync(Post post);
    Task RequestPostAsync(CreatePostDTO post);
    Task ApprovePostAsync(int id);
    Task RejectPostAsync(int id);
    Task UpdatePostAsync(CreatePostDTO post);
    Task DeletePostAsync(int id);
    Task<List<Post>> GetBookmarkedPostsAsync(string userId);
}