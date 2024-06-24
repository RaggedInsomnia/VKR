namespace CodeLauncherService.Services;

public class TempCodeFileCreator : ICodeFileCreator
{
    public async Task<string> CreateFileWithText(string sourceCode)
    {
        var fileName = Path.GetRandomFileName();
        fileName = Path.GetFileNameWithoutExtension(fileName);
        var fileNameCpp = $"{fileName}.c";

        await using var writer = File.CreateText(fileNameCpp);
        await writer.WriteAsync(sourceCode);

        return fileNameCpp;
    }

    public string GetObjectFileName(string cppFileName)
    {
        var fileName = Path.GetFileNameWithoutExtension(cppFileName);
        return $"{fileName}.o";
    }
}