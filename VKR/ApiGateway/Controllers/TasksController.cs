using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace ApiGateway.Controllers;

[Controller]
[Route("[controller]")]
public class TasksController : ControllerBase
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _appSettings;
    public TasksController(IHttpClientFactory httpClientFactory, IConfiguration appSettings)
    {
        _httpClient = httpClientFactory.CreateClient();
        _appSettings = appSettings;
    }

    [HttpGet("getTask")]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
    public async Task<string> GetTaskById(int taskId)
    {
        var connection = _appSettings.GetConnectionString("TaskService");
        var response = await _httpClient.GetAsync($"{connection}/Tasks/getTask/?taskId={taskId}");
        return await response.Content.ReadAsStringAsync();
    }

    [HttpGet("getTasks")]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
    public async Task<string> GetTasks()
    {
        var connection = _appSettings.GetConnectionString("TaskService");
        var response = await _httpClient.GetAsync($"{connection}/Tasks/getTasks");
        return await response.Content.ReadAsStringAsync();
    }

    [HttpGet("getTests")]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
    public async Task<string> GetTestsForTaskById(int taskId)
    {
        var connection = _appSettings.GetConnectionString("TaskService");
        var response = await _httpClient.GetAsync($"{connection}/Tasks/getTests/?taskId={taskId}");
        return await response.Content.ReadAsStringAsync();
    }
}