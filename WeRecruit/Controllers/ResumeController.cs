using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Net.Http.Headers;
using WeRecruit.Services;

namespace WeRecruit.Controllers;

public class ResumeController : Controller
{
    private readonly IResumeService _resumeService;

    public ResumeController(IResumeService resumeService)
    {
        _resumeService = resumeService;
    }

    [Route("/resume/{directory}")]
    [Authorize]
    public async Task<IActionResult> Read(string directory)
    {
        var (exists, stream) = await _resumeService.TryGet(directory);
        if (!exists) return NotFound();
        var _ = new FileExtensionContentTypeProvider().TryGetContentType(stream.Name, out var contentType);
        return new FileStreamResult(stream, MediaTypeHeaderValue.Parse(contentType));
    }
}