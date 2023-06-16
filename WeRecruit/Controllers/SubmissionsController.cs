using Microsoft.AspNetCore.Mvc;
using WeRecruit.Dto;
using WeRecruit.Services;

namespace WeRecruit.Controllers;

public class SubmissionsController : Controller
{
    private readonly ISubmissionsService _submissionsService;

    public SubmissionsController(ISubmissionsService submissionsService)
    {
        _submissionsService = submissionsService;
    }

    [Route("/")]
    public IActionResult Get()
    {
        return View("Index");
    }
    
    [Route("/")]
    [HttpPost]
    public async Task Post(SubmissionDto submissionDto)
    {
        var created = await _submissionsService.TryCreate(submissionDto);
        Response.Redirect($"/?result={(created ? "success" : "error")}");
    }
}