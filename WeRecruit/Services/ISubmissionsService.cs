using WeRecruit.Dto;

namespace WeRecruit.Services;

public interface ISubmissionsService
{
    Task<bool> TryCreate(SubmissionDto submissionDto);
}