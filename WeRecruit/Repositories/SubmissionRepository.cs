using WeRecruit.Data;
using WeRecruit.Entities;

namespace WeRecruit.Repositories;

public class SubmissionRepository : ISubmissionsRepository
{
    private readonly ApplicationDbContext _dbContext;


    public SubmissionRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> TryAddSubmission(Submission submission)
    {
        try
        {
            await _dbContext.Submissions.AddAsync(submission);
            await _dbContext.SaveChangesAsync();
        }
        catch
        {
            return false;
        }
        return true;
    }
}