using WeRecruit.Dto;
using WeRecruit.Entities;
using WeRecruit.Repositories;

namespace WeRecruit.Services;

public class SubmissionService : ISubmissionsService
{
    private readonly ISubmissionsRepository _submissionsRepository;
    private readonly IResumeService _resumeService;

    public SubmissionService(
        ISubmissionsRepository submissionsRepository,
        IResumeService resumeService
    )
    {
        _submissionsRepository = submissionsRepository;
        _resumeService = resumeService;
    }

    public async Task<bool> TryCreate(SubmissionDto submissionDto)
    {
        var capitalize = new Func<string, string>(str => char.ToUpper(str[0]) + str[1..].ToLower());

        var submission = new Submission
        {
            FirstName = capitalize(submissionDto.FirstName.Trim()),
            LastName = capitalize(submissionDto.LastName.Trim()),
            Email = submissionDto.Email.Trim(),
            Phone = submissionDto.Phone,
            LevelOfStudies = submissionDto.LevelOfStudies,
            YearsOfExperience = submissionDto.YearsOfExperience,
            LastEmployer = submissionDto.LastEmployer.ToUpper(),
        };
        submission.ResumeDirectoryName = $"{submission.FirstName}_{submission.LastName}";

        var resumeSaved = await _resumeService.TrySave(submission.ResumeDirectoryName, submissionDto.Resume);
        if (!resumeSaved) return false;

        var (submissionSaved, _) = await _submissionsRepository.TryCreate(submission);
        if (submissionSaved) return true;

        await _resumeService.TryDelete(submission.ResumeDirectoryName);
        return false;
    }

    public async Task<bool> TryDelete(int submissionId)
    {
        var (submissionDeleted, submission) = await _submissionsRepository.TryDelete(submissionId);
        if (!submissionDeleted) return false;

        await _resumeService.TryDelete(submission.ResumeDirectoryName);
        return true;
    }

    public async Task<IEnumerable<Submission>> ReadAll() => await _submissionsRepository.ReadAll();
}