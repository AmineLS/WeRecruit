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

    public async Task<bool> TryDelete(string targetDirectoryName)
    {
        var fullPath = Path.Combine(_parentDirectoryPath, targetDirectoryName);
        try
        {
            await Task.Run(() =>
            {
                if (Directory.Exists(fullPath)) Directory.Delete(fullPath, true);
            });
        }
        catch
        {
            return false;
        }

        return true;
    }

    public async Task<Tuple<bool, FileStream>> TryGet(string targetDirectoryName)
    {
        var path = Path.Combine(_parentDirectoryPath, targetDirectoryName);

        var noResult = Task.FromResult(Tuple.Create(false, (FileStream)null!));

        try
        {
            return await Task.Run(() =>
            {
                if (!Path.Exists(path)) return noResult;

                var files = Directory.GetFiles(path);
                return files.Length == 0
                    ? noResult
                    : Task.FromResult(
                        Tuple.Create(true, new FileStream(files.First(), FileMode.Open))
                    );
            });
        }
        catch
        {
            return await noResult;
        }
    }
}