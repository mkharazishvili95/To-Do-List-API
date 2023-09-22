using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using To_Do_List_API.Data;
using To_Do_List_API.Helpers;
using To_Do_List_API.Models;
using To_Do_List_API.Services;
using To_Do_List_API.Validation;

namespace To_Do_List_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppSettings _appSettings;
        private readonly ToDoListContext _context;
        private readonly IUserService _userService;
        public UserController(AppSettings appSettings, ToDoListContext context, IUserService userService)
        {
            _appSettings = appSettings;
            _context = context;
            _userService = userService;
        }
        [HttpPost("Register")]
        public IActionResult Register([FromBody] RegisterModel newUser)
        {
            var validator = new NewUserValidator(_context);
            var validatorResult = validator.Validate(newUser);
            if (!validatorResult.IsValid)
            {
                return BadRequest(validatorResult.Errors);
            }
            else
            {
                var user = newUser;
                _userService.Register(user);
                return Ok(new { Message = $"User: {user.UserName} has successfully registered!" });
            }
        }
        [HttpPost("Login")]
        public IActionResult Login([FromBody]LoginModel login)
        {
            var userLogin = _userService.Login(login);
            if (userLogin == null)
            {
                return BadRequest(new { message = "Username or Password is incorrect!" });
            }
            else
            {
                Loggs log = new Loggs
                {
                    UserLogged = $"User {userLogin.UserName} logged in.",
                    LoggDate = DateTime.Now
                };
                _context.Loggs.Add(log);
                _context.SaveChanges();


                var tokenString = GenerateToken(userLogin);

                return Ok(new
                {
                    Message = "You have successfully Logged!",
                    Id = userLogin.Id,
                    UserName = userLogin.UserName,
                    FirstName = userLogin.FirstName,
                    LastName = userLogin.LastName,
                    Role = userLogin.Role,
                    Token = tokenString
                });
            }
        }

        private string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.UserName.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(365),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            return tokenString;
        }
    }
}
