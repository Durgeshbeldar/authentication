using JWTdemo.Data;
using JWTdemo.DTOs;
using JWTdemo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWTdemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly MyContext _context;
        private readonly IConfiguration _configuration;
        public UserController(MyContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("Login")]
        public IActionResult Login(LoginDto login)
        {
            var existingUser = _context.Users.Include(user => user.Role).FirstOrDefault(user=> user.UserName == login.UserName);
            if (existingUser != null)
            {
                if(BCrypt.Net.BCrypt.Verify(login.Password, existingUser.PasswordHash))
                {
                    // token generation
                    var token = CreateToken(existingUser);
                    Response.Headers.Add("Jwt",token);
                    return Ok(new {
                        message= "Password Verified",
                        roleName = existingUser.Role.RoleName,
                    });
                }
                return BadRequest(new {messsage = "Password Doesnt Matched" });
            }
            return BadRequest(new {message = "User Not Found"});
        }

        private string CreateToken(User user)
        {
            var roleName = user.Role.RoleName;
            List<Claim> claims = new List<Claim>()
            {
                 new Claim(ClaimTypes.Name,user.UserName),
                 new Claim(ClaimTypes.Role, roleName),
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Key").Value));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            //token construction
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cred
                );
            //generate the token
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        
        }
    }
}
