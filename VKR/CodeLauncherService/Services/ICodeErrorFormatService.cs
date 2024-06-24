using CodeLauncherService.Models;

namespace CodeLauncherService.Services;

public interface ICodeErrorFormatService
{
    Output Format(Output source);
}