using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pet_Get.Interface;
using Pet_Get.Models;
using Pet_Get.Models.DTOs;

namespace Pet_Get.Controllers;

[Authorize]
public class PostController : Controller
{
    private readonly IPostRepo _postRepo;
    private readonly IAnimalTypeRepo _animalTypeRepo;
    private readonly IBookmarkRepo _bookmarkRepo;
    private readonly UserManager<AppUser> _userManager;

    public PostController(IPostRepo postRepo, IAnimalTypeRepo animalTypeRepo, IBookmarkRepo bookmarkRepo, UserManager<AppUser> userManager)
    {
        _postRepo = postRepo;
        _animalTypeRepo = animalTypeRepo;
        _bookmarkRepo = bookmarkRepo;
        _userManager = userManager;
    }

    [Authorize(Roles = "Admin")]

    [HttpGet]
    public async Task<IActionResult> EditPost(int id)
    {
        var animalTypes = await _animalTypeRepo.GetAllPostsAsync();
        var post = await _postRepo.GetPostByIdAsync(id);
        var dto = new CreatePostDTO
        {
            AnimalTypeId = post.AnimalTypeId,
            Post = post,
            AnimalTypes = animalTypes
        };

        return View(dto);
    }

    [Authorize(Roles = "Admin")]

    [HttpPost]
    public async Task<IActionResult> EditPost(CreatePostDTO post)
    {
        await _postRepo.UpdatePostAsync(post);
        return RedirectToAction("Posts", "Post");
    }

    [Authorize(Roles = "Admin")]

    public async Task<IActionResult> DeletePost(int id)
    {
        await _postRepo.DeletePostAsync(id);
        return RedirectToAction("Posts", "Post");
    }

    public async Task<IActionResult> Posts(PostsDTO dto)
    {
        if (String.IsNullOrEmpty(dto.search))
        {
            dto.Posts = await _postRepo.GetAllPostsAsync();
        }
        else
        {
            dto.Posts = await _postRepo.GetFilteredPostsAsync(dto.search);
        }

        var animalTypes = await _animalTypeRepo.GetAllPostsAsync();

        dto.AnimalTypes = animalTypes;

        return View(dto);
    }

    public async Task<IActionResult> Post(int id)
    {
        var pets = await _postRepo.GetPostByIdAsync(id);
        return View(pets);
    }

    [HttpGet]
    public async Task<IActionResult> RequestPost()
    {
        var animalTypes = await _animalTypeRepo.GetAllPostsAsync();
        var dto = new CreatePostDTO
        {
            AnimalTypes = animalTypes
        };
        return View(dto);
    }

    [HttpPost]
    public async Task<IActionResult> RequestPost(CreatePostDTO newPost)
    {
        await _postRepo.RequestPostAsync(newPost);
        if (User.IsInRole("admin-role"))
        {
        }

        return RedirectToAction("Posts", "Post");
    }

    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> ApprovePost(int id)
    {
        await _postRepo.ApprovePostAsync(id);

        return RedirectToAction("Post", new { id = id });
    }


    [Authorize(Roles = "Admin")]

    public async Task<IActionResult> RejectPost(int id)
    {
        await _postRepo.RejectPostAsync(id);

        return RedirectToAction("Posts");
    }

    public async Task<IActionResult> BookmarkPost(int postId)
    {
        var userId = _userManager.GetUserId(User);
        await _bookmarkRepo.AddBookmarkAsync(userId, postId);
        return RedirectToAction("Post", new { id = postId });
    }

    public async Task<IActionResult> RemoveBookmarkPost(int postId)
    {
        var userId = _userManager.GetUserId(User);
        await _bookmarkRepo.DeleteBookmarkAsync(userId, postId);
        return RedirectToAction("Post", new { id = postId });
    }
    
    public async Task<IActionResult> Bookmarks()
    {
        var userId = _userManager.GetUserId(User);
        var posts = await _postRepo.GetBookmarkedPostsAsync(userId);
        return View(posts);
    }
}