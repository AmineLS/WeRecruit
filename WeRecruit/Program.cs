using System.Net.Mail;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using WeRecruit.Data;
using WeRecruit.Entities;
using WeRecruit.Model;
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

builder.Services.AddScoped<ISubmissionsService, SubmissionService>();
builder.Services.AddScoped<ISubmissionsRepository, SubmissionRepository>();
builder.Services.AddSingleton<IResumeService, ResumeService>(_ =>
{
    var parentDirectory = builder.Configuration.GetValue<string>("FileBucket");
    return new ResumeService(parentDirectory ?? throw new ArgumentNullException());
});
builder.Services.AddSingleton<IMailService, MailService>(_ =>
{
    var smtpHost = builder.Configuration.GetValue<string>("SmtpServer:Host");
    var smtpPort = builder.Configuration.GetValue<int>("SmtpServer:Port");
    var smtpClient = new SmtpClient(smtpHost, smtpPort);
    var mailTemplate = builder.Configuration.GetSection("MailTemplate").Get<MailTemplate>();
    return new MailService(smtpClient, mailTemplate ?? throw new ArgumentNullException());
});
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

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();