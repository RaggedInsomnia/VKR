using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PlayerService.Repositories;
using Shared.Server.Data;

namespace AuthService.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly StudentRepository _studentRepository;
    private readonly TeacherRepository _teacherRepository;

    public AuthController(StudentRepository studentRepository, TeacherRepository teacherRepository)
    {
        _studentRepository = studentRepository;
        _teacherRepository = teacherRepository;
    }

    [HttpPost("loginStudent")]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
    public async Task<IResult> LoginStudent([FromForm] string login, [FromForm] string password)
    {
        var student = await _studentRepository.Students.FirstOrDefaultAsync(x => x.Login.Equals(login) && x.Password.Equals(password));
        if(student is null)
            return Results.Unauthorized();

        var claims = new List<Claim> { new (ClaimTypes.Name, login), new (ClaimTypes.Role, "student") };
        var JWT = new JwtSecurityToken(
            issuer: "AuthService",
            audience: "AuthClient",
            claims: claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(10)),
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("mysupersecret_secretsecretsecretkey!123")), SecurityAlgorithms.HmacSha256));

        var encodedJWT = new JwtSecurityTokenHandler().WriteToken(JWT);
        return Results.Accepted();
    }
    
}