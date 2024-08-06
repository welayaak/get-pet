using Pet_Get.Models;
using Pet_Get.Models.DTOs;

namespace Pet_Get.Interface;

public interface IForumRepo
{
    Task<List<Forum>> GetAllForumsAsync();
    Task<Forum> GetForumByIdAsync(int id);
    Task AddForumAsync(AddForumDTO forum);
}