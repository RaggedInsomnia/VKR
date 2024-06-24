using Microsoft.EntityFrameworkCore;
using Shared.Server.Data;

namespace PlayerService.Repositories;

public class TeacherRepository : DbContext
{
    public DbSet<TeacherData> Teachers { get; set; } = null! ;
    public TeacherRepository(DbContextOptions<TeacherRepository> options) : base(options)
    {
        Database.EnsureCreated();
    }
}