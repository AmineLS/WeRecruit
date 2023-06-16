using Microsoft.EntityFrameworkCore;
using WeRecruit.Data;
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

builder.Services.AddSingleton<ISubmissionsService, SubmissionService>();
builder.Services.AddSingleton<ISubmissionsRepository, SubmissionRepository>();
builder.Services.AddSingleton<IResumeService, ResumeService>(_ =>
{
    var parentDirectory = builder.Configuration.GetValue<string>("FileBucket");
    return new ResumeService(parentDirectory ?? throw new ArgumentNullException());
});
builder.Services.AddSingleton<IMailService, MailService>();


var app = builder.Build();

app.UseExceptionHandler("/Home/Error");

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

var appDbContext = app.Services.GetRequiredService<ApplicationDbContext>();
appDbContext.Database.EnsureDeleted();
await appDbContext.Database.EnsureCreatedAsync();

app.Run();