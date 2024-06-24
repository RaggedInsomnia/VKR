namespace PlayerService.Configurations;

public class UserServiceAppSettings
{
    public class ConnectionStringsSettings
    {
        public string CodeLauncherServiceEndpoint { get; set; }

        public Uri CodeLauncherServiceAddress => new(CodeLauncherServiceEndpoint);
    }

    public ConnectionStringsSettings ConnectionStrings { get; set; }
}