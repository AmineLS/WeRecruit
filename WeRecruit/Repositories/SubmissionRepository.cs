using Microsoft.EntityFrameworkCore;
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

    public async Task<Tuple<bool, Submission>> TryCreate(Submission submission)
    {
        try
        {
            var entityEntry = await _dbContext.Submissions.AddAsync(submission);
            await _dbContext.SaveChangesAsync();
            return Tuple.Create(true, entityEntry.Entity);
        }
        catch
        {
            return Tuple.Create(false, new Submission());
        }
    }

    public async Task<Tuple<bool, Submission>> TryDelete(int submissionId)
    {
        try
        {
            var submission = await _dbContext.Submissions.FindAsync(submissionId);
            _dbContext.Submissions.Remove(submission ?? throw new ArgumentException("No submission was found for the given Id."));
            await _dbContext.SaveChangesAsync();
            return Tuple.Create(true, submission);
        }
        catch
        {
            return Tuple.Create(false, new Submission());
        }
    }

    public async Task<IEnumerable<Submission>> ReadAll() => await _dbContext.Submissions.ToListAsync();
}