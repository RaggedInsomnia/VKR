using Shared.Server.Data;

namespace Shared.Server.Services;

public interface ICodeLauncherService
{
    Task<OutputDto> Launch(string sourceCode, string inputs);
}
