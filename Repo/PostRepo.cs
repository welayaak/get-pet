using Microsoft.EntityFrameworkCore;
using Pet_Get.Data;
using Pet_Get.Interface;
using Pet_Get.Models;
using Pet_Get.Models.DTOs;

namespace Pet_Get.Repo;

public class PostRepo : IPostRepo
{
    private readonly ApplicationDbContext _context;
    private readonly IAnimalTypeRepo _animalTypeRepo;

    public PostRepo(ApplicationDbContext context, IAnimalTypeRepo animalTypeRepo)
    {
        _context = context;
        _animalTypeRepo = animalTypeRepo;
    }

    public async Task<List<Post>> GetAllPostsAsync()
    {
        return await _context.Posts.Include(p => p.AnimalType).Where(p => p.Approved).OrderByDescending(p => p.Featured)
            .ThenByDescending(p => p.createdAt).ToListAsync();
    }

    public async Task<List<Post>> GetFilteredPostsAsync(string? search)
    {
        return await _context.Posts.Include(p => p.AnimalType)
            .Where(p => p.Approved
                        && (
                            !String.IsNullOrEmpty(search)
                            &&
                            (p.Title.ToLower().Contains(search.ToLower())
                             ||
                             p.Description.ToLower().Contains(search.ToLower())
                             ||
                             p.AnimalType.Type.ToLower().Contains(search.ToLower())
                            )
                        )
            )
            .OrderByDescending(p => p.Featured).ThenByDescending(p => p.createdAt).ToListAsync();
    }

    public async Task<List<Post>> GetAllPendingPostsAsync()
    {
        return await _context.Posts.Include(p => p.AnimalType).Where(p => !p.Approved)
            .OrderByDescending(p => p.Featured).ThenByDescending(p => p.createdAt).ToListAsync();
    }

    public async Task<Post> GetPostByIdAsync(int id)
    {
        return await _context.Posts.Include(p => p.Bookmarks).FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task RequestPostAsync(CreatePostDTO post)
    {
        if (post.Image != null && post.Image.Length > 0)
        {
            var newPost = new Post
            {
                Email = post.Email,
                Title = post.Title,
                Description = post.Description,
                Featured = false,
                Approved = false,
                PhoneNumber = post.PhoneNumber,
                createdAt = DateTime.Now,
            };

            if (post.AnimalTypeId == 0 && !String.IsNullOrEmpty(post.NewAnimalType))
            {
                var newAnimalType = await _animalTypeRepo.AddAnimalTypeAsync(post.NewAnimalType);
                newPost.AnimalTypeId = newAnimalType.Id;
            }
            else
            {
                newPost.AnimalTypeId = post.AnimalTypeId;
            }


            using (var memoryStream = new MemoryStream())
            {
                await post.Image.CopyToAsync(memoryStream);
                string base64String = Convert.ToBase64String(memoryStream.ToArray());
                string prefixedBase64String = $"data:image/jpeg;base64,{base64String}";

                newPost.Image = prefixedBase64String;
            }

            _context.Posts.Add(newPost);
            await _context.SaveChangesAsync();
        }
    }

    public async Task ApprovePostAsync(int id)
    {
        var post = await _context.Posts.FindAsync(id);

        if (post == null)
        {
            return;
        }

        post.Approved = true;

        _context.Update(post);
        await _context.SaveChangesAsync();
    }

    public async Task RejectPostAsync(int id)
    {
        var post = await _context.Posts.FindAsync(id);

        if (post == null)
        {
            return;
        }

        _context.Posts.Remove(post);
        await _context.SaveChangesAsync();
    }

    public async Task UpdatePostAsync(CreatePostDTO dto)
    {
        var post = await GetPostByIdAsync(dto.Id);

        if (dto.AnimalTypeId == 0 && !String.IsNullOrEmpty(dto.NewAnimalType))
        {
            var newAnimalType = await _animalTypeRepo.AddAnimalTypeAsync(dto.NewAnimalType);
            post.AnimalTypeId = newAnimalType.Id;
        }
        else
        {
            post.AnimalTypeId = dto.AnimalTypeId;
        }


        post.Title = dto.Title;
        post.Description = dto.Description;
        post.Email = dto.Email;
        post.Featured = dto.Featured;
        post.PhoneNumber = dto.PhoneNumber;
        post.editedAt = DateTime.Now;

        if (dto.Image != null && dto.Image.Length > 0)
        {
            using (var memoryStream = new MemoryStream())
            {
                await dto.Image.CopyToAsync(memoryStream);
                string base64String = Convert.ToBase64String(memoryStream.ToArray());
                string prefixedBase64String = $"data:image/jpeg;base64,{base64String}";

                post.Image = prefixedBase64String;
            }
        }

        _context.Update(post);
        await _context.SaveChangesAsync();
    }

    public async Task DeletePostAsync(int id)
    {
        var postToDelete = await _context.Posts.FindAsync(id);
        _context.Posts.Remove(postToDelete);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Post>> GetBookmarkedPostsAsync(string userId)
    {
        var postsIds = await _context.Bookmarks.Where(b => b.UserId.Equals(userId))
            .Select(p => p.PostId)
            .ToListAsync();

        var posts = await _context.Posts.Where(p => postsIds.Contains(p.Id)).Include(p => p.AnimalType).ToListAsync();

        return posts;
    }
}