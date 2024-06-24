using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.Server.Data;
using Shared.Server.Services;
using TaskService.Repositories;

namespace TaskService;

public class TaskHttpService : ITaskService
{
    private readonly TasksRepository _tasksRepository;
    public TaskHttpService(TasksRepository tasksRepository)
    {
        _tasksRepository = tasksRepository;
    }

    public async Task<TaskInfo> GetTask(int taskId)
    {
        var task = await _tasksRepository.Tasks.FirstOrDefaultAsync(x => x.Id == taskId);
        return task.ToTaskInfo();
    }

    public async Task<List<TaskInfo>> GetTasks()
    {
        var tasks = _tasksRepository.Tasks.ToList();
        List<TaskInfo> taskInfos = new();
        foreach(var task in tasks)
            taskInfos.Add(task.ToTaskInfo());
        return taskInfos;
    }

    public async Task<TestData> GetTestCases(int taskId)
    {
        return _tasksRepository.Tasks.FirstOrDefault(x => x.Id == taskId).ToTestData();
    }
}