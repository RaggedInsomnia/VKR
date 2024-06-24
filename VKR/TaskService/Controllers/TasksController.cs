using System.Net;
using Microsoft.AspNetCore.Mvc;
using Shared.Server.Data;
using Shared.Server.Services;

namespace TaskService.Controllers;

[ApiController]
[Route("[controller]")]
public class TasksController : ControllerBase
{
    private readonly ITaskService _taskService;

    public TasksController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpGet("getTask")]
    [ProducesResponseType(typeof(TaskInfo), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<TaskInfo>> GetTaskById(int taskId)
    {
        return await _taskService.GetTask(taskId);
    }

    [HttpGet("getTasks")]
    [ProducesResponseType(typeof(List<TaskInfo>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<List<TaskInfo>>> GetAllTasks()
    {
        return await _taskService.GetTasks();
    }

    [HttpGet("getTests")]
    [ProducesResponseType(typeof(TestData), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<TestData>> GetTestsForTask(int taskId)
    {
        return await _taskService.GetTestCases(taskId);
    }
}