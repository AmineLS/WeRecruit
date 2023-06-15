using Microsoft.AspNetCore.Mvc;

namespace WeRecruit.Controllers;

public class SubmissionsController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}