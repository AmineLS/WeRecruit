namespace WeRecruit.Services;

public interface IResumeService
{
    Task<bool> TrySave(string targetDirectoryName, IFormFile resumeFile);
    Task<bool> TryDelete(string targetDirectoryName);
    Task<Tuple<bool, FileStream>> TryGet(string targetDirectoryName);
}