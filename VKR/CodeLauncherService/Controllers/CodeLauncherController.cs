using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Shared.Server.Services;
using Newtonsoft.Json;
using Shared.Server.Data;

namespace CodeLauncherService.Controllers;

[Controller]
[Route("[controller]")]
public class CodeLauncherController : ControllerBase
{
    private readonly ICodeLauncherService _codeLauncherService;
    private readonly HttpClient _httpClient;

    public CodeLauncherController(ICodeLauncherService codeLauncherService, IHttpClientFactory httpClientFactory)
    {
        _codeLauncherService = codeLauncherService;
        _httpClient = httpClientFactory.CreateClient();
    }

    [HttpPost("verify")]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
    public async Task<string> VerifySolution([FromBody]string solutionData)
    {
        var solution = JsonConvert.DeserializeObject<SolutionData>(solutionData);
        var response = string.Empty;
        var testsString = await _httpClient.GetAsync($"http://localhost:5000/Tasks/getTests?taskId={solution.TaskId}");

        TestData tests = JsonConvert.DeserializeObject<TestData>(testsString.Content.ReadAsStringAsync().Result);

        for (int i = 0; i < tests.TestInput.Split("\n").Length; i++)
        {
            var testOutput = await _codeLauncherService.Launch(solution.SourceCode, tests.TestInput.Split("\n")[i]);

            if (!testOutput.Success)
            {
                response = $"Тест номер {i}. Ошибка: {testOutput.StandardError}";
                return response;
            }

            var test = int.Parse(tests.TestOutput.Split("\n")[i]);
            if (!Equals(testOutput.StandardOutput, test.ToString()))
            {
                response = $"Тест номер {i+1}. Ожидаемый ответ: {tests.TestOutput.Split("\n")[i]} | Полученный ответ: {testOutput.StandardOutput}";
                return response;
            }
        }

        response = "Все тесты пройдены успешно";
        return response;
    }
    
    // [HttpPost("verifyTest")]
    // [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
    // public async Task<ActionResult<string>> VerifySolution([FromForm]string sourceCode, int taskId)
    // {
    //     //var solution = JsonConvert.DeserializeObject<SolutionData>(solutionData);
    //     var response = string.Empty;
    //     var testsString = await _httpClient.GetAsync($"http://localhost:5000/Tasks/getTests?taskId={taskId}");
    //
    //     TestData tests = JsonConvert.DeserializeObject<TestData>(testsString.Content.ReadAsStringAsync().Result);
    //
    //     for (int i = 0; i < tests.TestInput.Split("\n").Length; i++)
    //     {
    //         var testOutput = await _codeLauncherService.Launch(sourceCode, tests.TestInput.Split("\n")[i]);
    //
    //         if (!testOutput.Success)
    //         {
    //             response = $"Тест номер {i}. Ошибка: {testOutput.StandardError}";
    //             return response;
    //         }
    //
    //         var test = int.Parse(tests.TestOutput.Split("\n")[i]);
    //         if (!Equals(testOutput.StandardOutput, test.ToString()))
    //         {
    //             response = $"Тест номер {i+1}. Ожидаемый ответ: {tests.TestOutput.Split("\n")[i]} | Полученный ответ: {testOutput.StandardOutput}";
    //             return response;
    //         }
    //     }
    //
    //     response = "Все тесты пройдены успешно";
    //     return Ok(response);
    // }
}