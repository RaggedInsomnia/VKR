namespace CodeLauncherService.Configurations;

public class CodeLauncherAppSettings
{
    public int ProcessTimeoutSec { get; set; }

    public TimeSpan ProcessTimeout => TimeSpan.FromSeconds(ProcessTimeoutSec);
}