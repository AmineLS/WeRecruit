using WeRecruit.Entities;

namespace WeRecruit.Repositories;

public interface ISubmissionsRepository
{
    Task<Tuple<bool, Submission>> TryCreate(Submission submission);
    Task<Tuple<bool, Submission>> TryDelete(int submissionId);
    Task<IEnumerable<Submission>> ReadAll();
}