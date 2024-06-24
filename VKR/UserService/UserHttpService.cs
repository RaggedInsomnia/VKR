using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PlayerService.Repositories;
using Shared.Server.Data;
using Shared.Server.Services;

namespace PlayerService;

public class UserHttpService : IUserService
{
    private readonly StudentRepository _studentRepository;
    private readonly TeacherRepository _teacherRepository;
    private readonly SolutionRepository _solutionRepository;
    private readonly IConfiguration _appSettings;
    private readonly HttpClient _httpClient;

    public UserHttpService(StudentRepository studentRepository, TeacherRepository teacherRepository,
        SolutionRepository solutionRepository, IConfiguration appSettings, IHttpClientFactory httpClientFactory)
    {
        _teacherRepository = teacherRepository;
        _studentRepository = studentRepository;
        _solutionRepository = solutionRepository;
        _appSettings = appSettings;
        _httpClient = httpClientFactory.CreateClient();
    }
    public async Task<TeacherData> GetTeacherById(int teacherId)
    {
        return await _teacherRepository.Teachers.FirstOrDefaultAsync(x => x.Id == teacherId);
    }

    public async Task<StudentData> GetStudentById(int studentId)
    {
        return await _studentRepository.Students.FirstOrDefaultAsync(x => x.Id == studentId);
    }

    public async Task<StudentSolutionData> GetLastStudentSolution(int studentId, int taskId)
    {
        return await _solutionRepository.Solutions.OrderBy(x => x.Attempt).LastOrDefaultAsync(x =>
            x.TaskId == taskId && x.StudentId == studentId);
    }

    public async Task<StudentSolutionData> GetLastSuccessfulStudentSolution(int studentId, int taskId)
    {
        return await _solutionRepository.Solutions.OrderBy(x => x.Attempt).LastOrDefaultAsync(x =>
            x.TaskId == taskId && x.StudentId == studentId && x.Success);
    }

    public async Task<string> VerifyStudentSolution(SolutionData solution)
    {
        var connection = _appSettings.GetConnectionString("CodeLauncherService");
        var content = JsonConvert.SerializeObject(solution);
        
        var response = await _httpClient.PostAsJsonAsync($"{connection}/CodeLauncher/verify", content);
        return await response.Content.ReadAsStringAsync();
    }
    public async Task<string> SaveSolution(int studentId, int variant, SolutionData solution)
    {
        var result = await VerifyStudentSolution(solution);
        var lastId = 0;
        var lastAttempt = 0;
        if (_solutionRepository.Solutions.Count() != 0)
        {
            lastId = _solutionRepository.Solutions.OrderBy(x=>x.Id).Last().Id;
            lastAttempt = _solutionRepository.Solutions.Where(x => x.StudentId == studentId && x.TaskId == solution.TaskId).OrderBy(x=>x.Attempt).Last().Attempt;
        }
        var savedSolution = new StudentSolutionData
        {
            Id = lastId+1,
            TaskId = solution.TaskId,
            Attempt = lastAttempt+1,
            StudentId = studentId,
            SourceCode = solution.SourceCode,
            Success = result.Equals("Все тесты пройдены успешно"),
            Variant = variant,
            Result = result
        };
        _solutionRepository.Solutions.Add(savedSolution);
        await _solutionRepository.SaveChangesAsync();

        return savedSolution.Result;
    }
}