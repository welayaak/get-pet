using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Pet_Get.Data;
using Pet_Get.Interface;
using Pet_Get.Models;
using Pet_Get.Models.DTOs;

namespace Pet_Get.Controllers;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly IPostRepo _postRepo;
    private readonly ApplicationDbContext _context;

    public AdminController(IPostRepo postRepo, ApplicationDbContext context)
    {
        _postRepo = postRepo;
        _context = context;
    }

    public async Task<IActionResult> Requests()
    {
        var posts = await _postRepo.GetAllPendingPostsAsync();
        return View(posts);
    }

    public async Task<IActionResult> Dashboard()
    {
        // Fetch posts including their AnimalType
        var postsWithAnimalTypes = await _context.Posts.Include(p => p.AnimalType).ToListAsync();

        // Group by AnimalTypeId
        var groupedPosts = postsWithAnimalTypes.GroupBy(p => p.AnimalType.Id).ToList();

        // Prepare data for the doughnut chart
        var dataPoints = groupedPosts.Select(g => new { y = g.Count(), label = g.First().AnimalType.Type }).ToArray();

        // used for animal types graph
        ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);

        //used for number of users graph
        var numberOfUsers = await _context.Users.CountAsync();
        //used for pending posts graph
        var numberOfPendingPosts = await _context.Posts.Where(p => p.Approved == false).CountAsync();

        var dto = new AdminDashboardDTO
        {
            userCount = numberOfUsers,
            pendingPostsCount = numberOfPendingPosts
        };
        return View(dto);
    }
}