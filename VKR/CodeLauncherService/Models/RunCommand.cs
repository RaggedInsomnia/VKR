namespace CodeLauncherService.Models;

public readonly struct RunCommand
{
    public static RunCommand CreateCompile(string codeFileName, string objectFileName)
    {
        if (Environment.OSVersion.Platform == PlatformID.Win32NT)
        {
            return new RunCommand("wsl", $"g++ {codeFileName} -Wall -o {objectFileName} -static-libgcc -static-libstdc++");
        }

        return new RunCommand("g++", $"-g {codeFileName} -Wall -o {objectFileName} -static-libgcc -static-libstdc++");
    }

    public static RunCommand CreateLaunch(string objectFileName, string inputs)
    {
        var input = inputs != null ? string.Join(" ", inputs) : string.Empty;
        if (Environment.OSVersion.Platform == PlatformID.Win32NT)
        {
            return new RunCommand("wsl", $"./{objectFileName}", input);
        }

        return new RunCommand($"./{objectFileName}", string.Empty, input);
    }

    public static RunCommand DeleteFile(string fileName)
    {
        if (Environment.OSVersion.Platform == PlatformID.Win32NT)
        {
            return new("wsl", $"rm {fileName}");
        }

        return new("rm", $"{fileName}");
    }

    public string Command { get; }
    public string Arguments { get; }
    public string Input { get; }

    private RunCommand(string command, string arguments, string input = null)
    {
        Command = command;
        Arguments = arguments;
        Input = input;
    }

    public override string ToString()
    {
        var log = $"{Command} {Arguments}";
        if (!string.IsNullOrEmpty(Input))
        {
            log = $"{log}, {nameof(Input)}: {Input}";
        }

        return log;
    }
}