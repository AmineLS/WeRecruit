namespace WeRecruit.Services;

public class ResumeService : IResumeService
{
    private readonly string _parentDirectoryPath;

    public ResumeService(string parentDirectoryPath)
    {
        _parentDirectoryPath = parentDirectoryPath;
    }

    public async Task<bool> TrySave(string targetDirectoryName, IFormFile resumeFile)
    {
        var directoryPath = Path.Combine(_parentDirectoryPath, targetDirectoryName);
        var filePath = Path.Combine(directoryPath, $"resume{Path.GetExtension(resumeFile.FileName)}");
        try
        {
            if (!Directory.Exists(directoryPath)) Directory.CreateDirectory(directoryPath);
            await using var fileStream = new FileStream(filePath, FileMode.Create);
            await resumeFile.CopyToAsync(fileStream);
        }
        catch
        {
            return false;
        }
        return true;
    }

    public Task<bool> TryDelete(string targetDirectoryName)
    {
        var fullPath = Path.Combine(_parentDirectoryPath, targetDirectoryName);
        try
        {
            if (Directory.Exists(fullPath)) Directory.Delete(fullPath, true);
        }
        catch
        {
            return Task.FromResult(false);
        }

        return Task.FromResult(true);
    }
}