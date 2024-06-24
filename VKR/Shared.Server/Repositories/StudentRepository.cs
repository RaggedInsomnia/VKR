using Microsoft.EntityFrameworkCore;
using Shared.Server.Data;

namespace PlayerService.Repositories;

public class StudentRepository : DbContext
{
    public DbSet<StudentData> Students { get; set; } = null! ;
    public StudentRepository(DbContextOptions<StudentRepository> options) : base(options)
    {
        Database.EnsureCreated();
    }
}