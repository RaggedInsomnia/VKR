namespace ApiGateway.Configurations;

public class ApiGatewayAppSettings
{
    public class ConnectionStringsSettings
    {
        public string CodeLauncherServiceEndpoint { get; set; }
        public string TaskServiceEndpoint { get; set; }

        public Uri CodeLauncherServiceAddress => new(CodeLauncherServiceEndpoint);
        public Uri TaskServiceAddress => new(TaskServiceEndpoint);
    }

    public ConnectionStringsSettings ConnectionStrings { get; set; }
}