using Microsoft.EntityFrameworkCore;
using Shared.Server.Data;

namespace TaskService.Repositories;

public class TasksRepository : DbContext
{
    public DbSet<TaskData> Tasks { get; set; } = null! ;
    public TasksRepository(DbContextOptions<TasksRepository> options) : base(options)
    {
        Database.EnsureCreated();
    }
}