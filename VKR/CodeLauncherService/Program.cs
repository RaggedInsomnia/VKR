using CodeLauncherService;
using CodeLauncherService.Configurations;
using CodeLauncherService.Services;
using Shared.Server.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.Configure<CodeLauncherAppSettings>(builder.Configuration);

builder.Services.AddHttpClient();
builder.Services.AddScoped<ICodeLauncherService, CodeLauncherHttpService>();
builder.Services.AddScoped<ICodeFileCreator, TempCodeFileCreator>();
builder.Services.AddScoped<IProcessExecutorProxy, ConsoleExecutorProxy>();
builder.Services.AddScoped<ICodeErrorFormatService, CodeErrorFormatService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();