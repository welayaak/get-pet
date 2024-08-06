using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Pet_Get.Models;

namespace Pet_Get.Data;

public class ApplicationDbContext : IdentityDbContext<AppUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<AppUser> AppUsers { get; set; } = null!;

    public DbSet<Post> Posts { get; set; }
    public DbSet<AnimalType> AnimalTypes { get; set; }
    public DbSet<Forum> Forums { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Bookmark> Bookmarks { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            { Name = "Admin", NormalizedName = "ADMIN", Id = "admin-role" });
        modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            { Name = "User", NormalizedName = "USER", Id = "user-role" });


        modelBuilder.Entity<AppUser>().HasData(
            new AppUser
            {
                Id = "admin-user",
                Email = "admin@mail.com",
                NormalizedEmail = "ADMIN@MAIL.COM",
                NormalizedUserName = "ADMIN@MAIL.COM",
                UserName = "admin@mail.com",
                PasswordHash = new PasswordHasher<AppUser>().HashPassword(
                    new AppUser { Email = "admin@mail.com", UserName = "admin" },
                    "admin")
            }
        );


        modelBuilder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string> { UserId = "admin-user", RoleId = "admin-role" }
        );

        modelBuilder.Entity<AnimalType>().HasData(
            new AnimalType
            {
                Id = 1,
                Type = "Cat"
            },
            new AnimalType
            {
                Id = 2,
                Type = "Dog"
            }
            );

        modelBuilder.Entity<Post>().HasData(
            new Post
            {
                Id = 1,
                Title = "Meet Bella – The Lovable Labrador Retriever",
                Description =
                    "Bella is a 3-year-old Labrador Retriever with a heart full of love and a wagging tail that never stops. She's great with kids and other pets. Bella is looking for a forever home where she can share her joy and energy. Adopt Bella today and experience unconditional love!",
                Image =
                    "/images/Persian+Cat+Facts+History+Personality+and+Care+_+ASPCA+Pet+Health+Insurance+_+white+Persian+cat+resting+on+a+brown+sofa-min.jpg",
                Email = "test@mail.com",
                Featured = false,
                AnimalTypeId = 1,
                Approved = true,
                PhoneNumber = "123456",
                createdAt = DateTime.Now.AddDays(-5),

            },
            new Post
            {
                Id = 2,
                Title = "Fluffy the Persian Cat Needs a Loving Home",
                Description =
                    "Fluffy is a gentle and affectionate 2-year-old Persian cat. She loves to cuddle and enjoys quiet afternoons lounging in the sun. Fluffy is spayed, vaccinated, and ready to be your purr-fect companion. Give Fluffy a chance to be part of your family!",
                Image = "/images/360_F_610716498_li6BIgt75TXw8B4W89pbf3VtKgHNQkXo.jpg",
                Email = "test@mail.com",
                Featured = false,
                AnimalTypeId = 2,
                Approved = true,
                PhoneNumber = "123456",
                createdAt = DateTime.Now,
            },
            new Post
            {
                Id = 3,
                Title = "Senior Sweetheart: Meet Charlie the Beagle",
                Description =
                    "Charlie is a 10-year-old Beagle with a mellow temperament and a love for leisurely walks. Despite his age, Charlie is healthy and full of love to give. He deserves a cozy home to spend his golden years. Adopt Charlie and enjoy his gentle companionship.",
                Image = "/images/Labrador_Retriever_portrait.jpg",
                Email = "test@mail.com",
                Featured = false,
                AnimalTypeId = 2,
                Approved = true,
                PhoneNumber = "123456",
                createdAt = DateTime.Now,
            },
            new Post
            {
                Id = 4,
                Title = "Sweet Sophia the Siamese Cat Awaits Her Forever Home",
                Description =
                    "Sophia is a 3-year-old Siamese cat with striking blue eyes and a sweet personality. She loves to play and is very affectionate. Sophia is spayed, vaccinated, and ready to find her forever family. Bring Sophia home and let her light up your life!",
                Image = "/images/image-83814-800.jpg",
                Email = "test@mail.com",
                Featured = false,
                AnimalTypeId = 1,
                Approved = true,
                PhoneNumber = "123456",
                createdAt = DateTime.Now,
            }
        );
    }
}