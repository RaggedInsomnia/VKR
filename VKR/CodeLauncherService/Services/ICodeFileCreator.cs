namespace CodeLauncherService.Services;

public interface ICodeFileCreator
{
    Task<string> CreateFileWithText(string sourceCode);
    string GetObjectFileName(string cppFileName);
}