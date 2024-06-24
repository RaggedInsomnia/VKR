using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shared.Server.Data;


namespace ApiGateway.Controllers;

[Controller]
[Route("[controller]")]
public class SolutionController : ControllerBase
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _appSettings;
    public SolutionController(IHttpClientFactory httpClientFactory, IConfiguration appSettings)
    {
        _httpClient = httpClientFactory.CreateClient();
        _appSettings = appSettings;
    }

    [HttpPost("verifySolution")]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<string>> VerifySolution([FromForm] string sourceCode, [FromForm]int taskId, [FromForm]int studentId, [FromForm]int variant)
    {
        var connection = _appSettings.GetConnectionString("UserService");
        if (string.IsNullOrEmpty(sourceCode))
        {
            throw new ArgumentNullException(nameof(sourceCode));
        }

        var solution = new SolutionData
        {
            TaskId = taskId,
            SourceCode = sourceCode
        };
        var content = JsonConvert.SerializeObject(solution);
        
        var response = await _httpClient.PostAsJsonAsync($"{connection}/UserService/saveSolution?studentId={studentId}&variant={variant}", content); 
        return await response.Content.ReadAsStringAsync();
    }

    [HttpGet("getLastStudentSolution")]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
    public async Task<string> GetLastStudentSolution([FromForm] int studentId, [FromForm] int taskId)
    {
        var connection = _appSettings.GetConnectionString("UserService");
        var response = await _httpClient.GetAsync($"{connection}/UserService/getLastStudentSolution?studentId={studentId}&taskId={taskId}");
        return await response.Content.ReadAsStringAsync();
    }
    
    [HttpGet("getLastSuccessfulStudentSolution")]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
    public async Task<string> GetLastSuccessfulStudentSolution([FromForm] int studentId, [FromForm] int taskId)
    {
        var connection = _appSettings.GetConnectionString("UserService");
        var response = await _httpClient.GetAsync($"{connection}/UserService/getLastSuccessfulStudentSolution?studentId={studentId}&taskId={taskId}");
        return await response.Content.ReadAsStringAsync();
    }
}