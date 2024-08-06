using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Pet_Get.Data;
using Pet_Get.Interface;
using Pet_Get.Models;
using Pet_Get.Repo;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
                       throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<AppUser>(options => 
    {
        options.Password.RequiredLength = 1; // Minimum password length
        options.Password.RequireDigit = false; // Require at least one digit
        options.Password.RequireLowercase = false; // Require at least one lowercase character
        options.Password.RequireUppercase = false; // Require at least one uppercase character
        options.Password.RequireNonAlphanumeric = false; // Require at least one non-alphanumeric character
        options.Password.RequiredUniqueChars = 0; // Require at least four unique characters
        options.SignIn.RequireConfirmedAccount = false;
        options.SignIn.RequireConfirmedEmail = false;
        options.SignIn.RequireConfirmedPhoneNumber = false;
        options.SignIn.RequireConfirmedEmail = false;
    })
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllersWithViews();


// register repos
// builder.Services.AddScoped<IAppUserRepo, AppUserRepo>();
builder.Services.AddScoped<IPostRepo, PostRepo>();
builder.Services.AddScoped<IAnimalTypeRepo, AnimalTypeRepo>();
builder.Services.AddScoped<IBookmarkRepo, BookmarkRepo>();
builder.Services.AddScoped<IForumRepo, ForumRepo>();
builder.Services.AddScoped<ICommentRepo, CommentRepo>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();