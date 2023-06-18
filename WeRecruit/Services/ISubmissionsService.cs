using WeRecruit.Dto;
using WeRecruit.Entities;

namespace WeRecruit.Services;

public interface ISubmissionsService
{
    Task<bool> TryCreate(SubmissionDto submissionDto);
    Task<bool> TryDelete(int submissionId);
    Task<IEnumerable<Submission>> ReadAll();
}