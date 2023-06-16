using WeRecruit.Entities;

namespace WeRecruit.Repositories;

public interface ISubmissionsRepository
{
    Task<bool> TryAddSubmission(Submission submission);
}