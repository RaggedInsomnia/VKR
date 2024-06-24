using Microsoft.EntityFrameworkCore;
using Shared.Server.Data;

namespace PlayerService.Repositories;

public class SolutionRepository : DbContext
{
    public DbSet<StudentSolutionData> Solutions { get; set; } = null! ;
    public SolutionRepository(DbContextOptions<SolutionRepository> options) : base(options)
    {
        Database.EnsureCreated();
    }
}