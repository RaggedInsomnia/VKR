using Shared.Server.Data;

namespace Shared.Server.Services;

public interface ITaskService
{
    Task<List<TaskInfo>> GetTasks();
    Task<TaskInfo> GetTask(int taskId);
    Task<TestData> GetTestCases(int taskId);
}