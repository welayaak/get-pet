using Pet_Get.Models;

namespace Pet_Get.Interface;

public interface ICommentRepo
{
    Task AddCommentAsync(Comment comment);
    Task<List<Comment>> GetCommentsByForumIdAsync(int forumId);
}