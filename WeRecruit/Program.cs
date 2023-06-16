using Microsoft.EntityFrameworkCore;
using WeRecruit.Data;

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


var app = builder.Build();

app.UseExceptionHandler("/Home/Error");

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

await app.Services.GetRequiredService<ApplicationDbContext>().Database.EnsureCreatedAsync();

app.Run();