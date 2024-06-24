namespace Shared.Server.Data;

public class TaskData
{
    public int Id { get; set; }
    public int TaskNumber { get; set; }
    public string Description { get; set; }
    public string TestInput { get; set; }
    public string TestOutput { get; set; }

    public TestData ToTestData()
    {
        return new TestData
        {
            TestInput = TestInput,
            TestOutput = TestOutput
        };
    }

    public TaskInfo ToTaskInfo()
    {
        return new TaskInfo
        {
            TaskNumber = TaskNumber,
            Description = Description
        };
    }
}
