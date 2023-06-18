using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WeRecruit.Services;

namespace WeRecruit.Controllers;

public class HomeController : Controller
{
    private readonly ISubmissionsService _submissionsService;

    public HomeController(ISubmissionsService submissionsService)
    {
        _submissionsService = submissionsService;
    }

    [Route("/home")]
    [Authorize]
    public async Task<IActionResult> Read()
    {
        ViewData["UserName"] = User.Identity?.Name;
        var submissions = await _submissionsService.ReadAll();
        return View("Index", submissions);
    }
}