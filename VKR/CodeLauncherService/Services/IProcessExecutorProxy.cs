using CodeLauncherService.Models;

namespace CodeLauncherService.Services
{
    public interface IProcessExecutorProxy
    {
        Task<Output> Run(RunCommand command);
    }
}