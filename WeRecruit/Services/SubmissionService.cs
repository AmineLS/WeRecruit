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
        IResumeService resumeService,
        IMailService mailService
    )
    {
        _submissionsRepository = submissionsRepository;
        _resumeService = resumeService;
    }

    public async Task<bool> TryCreate(SubmissionDto submissionDto)
    {
        var capitalize = new Func<string, string>(str => char.ToUpper(str[0]) + str[1..]);

        var submission = new Submission
        {
            FirstName = capitalize(submissionDto.FirstName.Trim()),
            LastName = capitalize(submissionDto.LastName.Trim()),
            Email = submissionDto.Email.Trim(),
            Phone = submissionDto.Phone,
            LevelOfStudies = submissionDto.LevelOfStudies,
            YearsOfExperience = submissionDto.YearsOfExperience,
            LastEmployer = submissionDto.LastEmployer.ToUpper(),
            ResumeDirectoryName = $"{submissionDto.FirstName}_{submissionDto.LastName}"
        };

        var saveResume = await _resumeService.TrySave(submission.ResumeDirectoryName, submissionDto.Resume);
        if (!saveResume) return false;

        var saveSubmission = await _submissionsRepository.TryAddSubmission(submission);
        if (saveSubmission) return true;

        await _resumeService.TryDelete(submission.ResumeDirectoryName);
        return false;
    }

        _mailService.SendConfirmation(submission.Email);

        return true;
    }
}