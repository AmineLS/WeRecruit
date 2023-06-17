using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using WeRecruit.Dto;
using WeRecruit.Services;

namespace WeRecruit.Controllers;

public class AuthController : Controller
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [Route("/login")]
    public IActionResult Get()
    {
        return View("Index");
    }

    [HttpPost]
    [Route("/login")]
    public async Task<IActionResult> Post(LoginDto loginDto)
    {
        var (authenticated, admin) = await _authService.TryAuthenticateAdmin(loginDto);

        if (!authenticated) return Redirect("/login?result=error");

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(
                new ClaimsIdentity(
                    new[]
                    {
                        new Claim(ClaimTypes.Name, admin.Name)
                        // other claims as needed
                    },
                    CookieAuthenticationDefaults.AuthenticationScheme
                )
            )
        );
        return Redirect("/home");
    }
}