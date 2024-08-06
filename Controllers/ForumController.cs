using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pet_Get.Interface;
using Pet_Get.Models;
using Pet_Get.Models.DTOs;

namespace Pet_Get.Controllers;
public class ForumController : Controller
{
    private readonly IForumRepo _forumRepo;
    private readonly ICommentRepo _commentRepo;

    public ForumController(IForumRepo forumRepo, ICommentRepo commentRepo)
    {
        _forumRepo = forumRepo;
        _commentRepo = commentRepo;
    }

    public async Task<IActionResult> Forums()
    {
        var forums = await _forumRepo.GetAllForumsAsync();
        return View(forums);
    }
    [HttpGet]
    public async Task<IActionResult> AddForum()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> AddForum(AddForumDTO forum)
    {
        await _forumRepo.AddForumAsync(forum);
        return RedirectToAction("Forums");
    }
    public async Task<IActionResult> Forum(int postId)
    {
        var forum = await _forumRepo.GetForumByIdAsync(postId);
        var dto = new ForumDTO
        {
            Forum = forum
        };
        return View(dto);
    }
    [HttpPost]
    public async Task<IActionResult> Comment(ForumDTO dto)
    {
        var comment = new Comment
        {
            ForumId = dto.Comment.ForumId,
            Body = dto.Comment.Body,
            createdAt = DateTime.Now,
        };
        
        await _commentRepo.AddCommentAsync(comment);
        return RedirectToAction("Forum", new {postId = dto.Comment.ForumId});
    }
}