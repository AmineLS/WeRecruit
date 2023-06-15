var builder = WebApplication.CreateBuilder(args);


builder.Services
    .AddControllersWithViews()
    .AddRazorRuntimeCompilation();

var app = builder.Build();

app.UseExceptionHandler("/Home/Error");

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Submissions}/{action=Index}/{id?}");

app.Run();