using System.Net;
using Microsoft.AspNetCore.Mvc;
using Shared.Server.Data;
using Shared.Server.Services;
using Newtonsoft.Json;

namespace PlayerService.Controllers;

[ApiController]
[Route("[controller]")]
public class UserServiceController : ControllerBase
{
    private readonly IUserService _userService;
    public UserServiceController(IUserService userService)
    {
        _userService = userService;
    }
    
    [HttpGet("getTeacher")]
    [ProducesResponseType(typeof(TeacherData), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<TeacherData>> GetTeacherById(int teacherId)
    {
        return await _userService.GetTeacherById(teacherId);
    }
    
    [HttpGet("getStudent")]
    [ProducesResponseType(typeof(StudentData), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<StudentData>> GetStudentById(int studentId)
    {
        return await _userService.GetStudentById(studentId);
    }


    [HttpGet("getLastStudentSolution")]
    [ProducesResponseType(typeof(StudentSolutionData), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<StudentSolutionData>> GetLastStudentSolution(int studentId, int taskId)
    {
        return await _userService.GetLastStudentSolution(studentId, taskId);
    }
    
    [HttpGet("getLastSuccessfulStudentSolution")]
    [ProducesResponseType(typeof(StudentSolutionData), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<StudentSolutionData>> GetLastSuccessfulStudentSolution(int studentId, int taskId)
    {
        return await _userService.GetLastSuccessfulStudentSolution(studentId, taskId);
    }

    [HttpPost("saveSolution")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    //public async Task<string> VerifyAndSaveStudentSolution([FromForm] string sourceCode, [FromForm]int taskId, [FromForm]int studentId, [FromForm]int variant)
    public async Task<string> VerifyAndSaveStudentSolution([FromBody] string solutionData, int studentId, int variant)
    {
        var solution = JsonConvert.DeserializeObject<SolutionData>(solutionData);
        // var solution = new SolutionData
        // {
        //     TaskId = taskId,
        //     SourceCode = sourceCode
        // };
        return await _userService.SaveSolution(studentId, variant, solution);
    }
}