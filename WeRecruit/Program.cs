var builder = WebApplication.CreateBuilder(args);


builder.Services
    .AddControllersWithViews()
    .AddRazorRuntimeCompilation();

var app = builder.Build();

app.UseExceptionHandler("/Home/Error");

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

await app.Services.GetRequiredService<ApplicationDbContext>().Database.EnsureCreatedAsync();

app.Run();