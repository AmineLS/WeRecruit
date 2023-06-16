using Microsoft.AspNetCore.Mvc;

namespace WeRecruit.Controllers;

public class SubmissionsController : Controller
{
    [Route("/")]
    public IActionResult Get()
    {
        return View("Index");
    }
    
    [Route("/")]
    [HttpPost]
    public void Index(SubmissionDto submissionDto)
    {
        
        Response.Redirect("/");
    }
}