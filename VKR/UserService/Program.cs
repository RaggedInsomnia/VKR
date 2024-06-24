using Microsoft.EntityFrameworkCore;
using PlayerService;
using PlayerService.Configurations;
using PlayerService.Repositories;
using Shared.Server.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<UserServiceAppSettings>(builder.Configuration);
// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddHttpClient();
builder.Services.AddScoped<IUserService, UserHttpService>();
builder.Services.AddDbContext<StudentRepository>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Database")));
builder.Services.AddDbContext<TeacherRepository>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Database")));
builder.Services.AddDbContext<SolutionRepository>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Database")));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();