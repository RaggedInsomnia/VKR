using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace ApiGateway.Controllers;


[Controller]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _appSettings;
    public UserController(IHttpClientFactory httpClientFactory, IConfiguration appSettings)
    {
        _httpClient = httpClientFactory.CreateClient();
        _appSettings = appSettings;
    }

    [HttpGet("getStudent")]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
    public async Task<string> GetStudentById(int studentId)
    {
        var connection = _appSettings.GetConnectionString("UserService");
        var response = await _httpClient.GetAsync($"{connection}/UserService/getStudent/?studentId={studentId}");
        return await response.Content.ReadAsStringAsync();
    }
    
    [HttpGet("getTeacher")]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
    public async Task<string> GetTeacherById(int teacherId)
    {
        var connection = _appSettings.GetConnectionString("UserService");
        var response = await _httpClient.GetAsync($"{connection}/UserService/getTeacher/?teacherId={teacherId}");
        return await response.Content.ReadAsStringAsync();
    }
    
}