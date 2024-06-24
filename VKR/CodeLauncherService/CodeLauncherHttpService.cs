using CodeLauncherService.Models;
using CodeLauncherService.Services;
using Shared.Server.Data;
using Shared.Server.Services;

namespace CodeLauncherService;

public class CodeLauncherHttpService : ICodeLauncherService
{
    private readonly IProcessExecutorProxy _processExecutorProxy;
    private readonly ICodeFileCreator _codeFileCreator;
    private readonly ICodeErrorFormatService _errorFormatService;

    public CodeLauncherHttpService(IProcessExecutorProxy processExecutorProxy, ICodeFileCreator codeFileCreator,
        ICodeErrorFormatService errorFormatService)
    {
        _codeFileCreator = codeFileCreator;
        _processExecutorProxy = processExecutorProxy;
        _errorFormatService = errorFormatService;
    }
    
    public async Task<OutputDto> Launch(string sourceCode, string inputs = null)
    {
        var compileResult = await CompileCode(sourceCode);
        if (compileResult.Output.HasError)
        {
            await _processExecutorProxy.Run(RunCommand.DeleteFile(compileResult.ObjectFileName));
            await _processExecutorProxy.Run(RunCommand.DeleteFile($"{Path.GetFileNameWithoutExtension(compileResult.ObjectFileName)}.c"));
            return new OutputDto
            {
                StandardError = compileResult.Output.StandardError
            };
        }

        var launchOutput = await LaunchCode(compileResult.ObjectFileName, inputs);
        launchOutput = _errorFormatService.Format(launchOutput);

        await _processExecutorProxy.Run(RunCommand.DeleteFile(compileResult.ObjectFileName));
        await _processExecutorProxy.Run(RunCommand.DeleteFile($"{Path.GetFileNameWithoutExtension(compileResult.ObjectFileName)}.c"));

        return new OutputDto
        {
            StandardOutput = launchOutput.StandardOutput,
            StandardError = launchOutput.StandardError
        };
    }

    private async Task<CompileCppResult> CompileCode(string sourceCode)
    {
        var cppFileName = await _codeFileCreator.CreateFileWithText(sourceCode);
        var objectFileName = _codeFileCreator.GetObjectFileName(cppFileName);

        var output = await _processExecutorProxy.Run(RunCommand.CreateCompile(cppFileName, objectFileName));

        return new CompileCppResult
        {
            Output = output,
            ObjectFileName = objectFileName
        };
    }

    private async Task<Output> LaunchCode(string objectFileName, string inputs)
    {
        return await _processExecutorProxy.Run(RunCommand.CreateLaunch(objectFileName, inputs));
    }
}