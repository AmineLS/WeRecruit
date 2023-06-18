using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WeRecruit.Dto;
using WeRecruit.Services;

namespace WeRecruit.Controllers;

public class SubmissionsController : Controller
{
    private readonly ISubmissionsService _submissionsService;
    private readonly IMailService _mailService;

    public SubmissionsController(ISubmissionsService submissionsService, IMailService mailService)
    {
        _submissionsService = submissionsService;
        _mailService = mailService;
    }

    [Route("/")]
    public IActionResult Read()
    {
        return View("Index");
    }
    
    [Route("/")]
    [HttpPost]
    public async Task Create(SubmissionDto submissionDto)
    {
        var created = await _submissionsService.TryCreate(submissionDto);
        if (created) _mailService.SendConfirmation(submissionDto.Email.Trim());
        Response.Redirect($"/?result={(created ? "success" : "error")}");
    }

    [Route("/submissions/{submissionId:int}/delete")]
    [HttpPost]
    [Authorize]
    public async Task Delete(int submissionId)
    {
        var deleted = await _submissionsService.TryDelete(submissionId);
        Response.Redirect($"/home?result={(deleted ? "success" : "error")}");
    }
}