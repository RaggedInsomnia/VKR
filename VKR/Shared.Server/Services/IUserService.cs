using Microsoft.AspNetCore.Mvc;
using Shared.Server.Data;

namespace Shared.Server.Services;

public interface IUserService
{
    Task<TeacherData> GetTeacherById(int teacherId);
    Task<StudentData> GetStudentById(int studentId);
    Task<StudentSolutionData> GetLastStudentSolution(int studentId, int taskId);
    Task<StudentSolutionData> GetLastSuccessfulStudentSolution(int studentId, int taskId);
    
    Task<string> VerifyStudentSolution(SolutionData solution);
    Task<string> SaveSolution(int studentId, int variant, SolutionData solution);
}