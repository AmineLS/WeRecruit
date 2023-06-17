using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using WeRecruit.Data;
using WeRecruit.Entities;
using WeRecruit.Repositories;
using WeRecruit.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services
    .AddControllersWithViews()
    .AddRazorRuntimeCompilation();

builder.Services
    .AddDbContext<ApplicationDbContext>(options =>
    {
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        options.UseSqlServer(connectionString);
    });

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options => options.LoginPath = "/login");

builder.Services.AddSingleton<ISubmissionsService, SubmissionService>();
builder.Services.AddSingleton<ISubmissionsRepository, SubmissionRepository>();
builder.Services.AddSingleton<IResumeService, ResumeService>(_ =>
{
    var parentDirectory = builder.Configuration.GetValue<string>("FileBucket");
    return new ResumeService(parentDirectory ?? throw new ArgumentNullException());
});
builder.Services.AddSingleton<IMailService, MailService>();
builder.Services.AddSingleton<IAuthService, AuthService>(_ =>
{
    var admins = builder
        .Configuration
        .GetSection("Admins")
        .Get<IEnumerable<Admin>>()?
        .ToHashSet();
    return new AuthService(admins ?? throw new ArgumentNullException());
});

var app = builder.Build();

app.UseExceptionHandler("/Home/Error");

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.MapControllers();

var appDbContext = app.Services.GetRequiredService<ApplicationDbContext>();
appDbContext.Database.EnsureDeleted();
await appDbContext.Database.EnsureCreatedAsync();

app.Run();