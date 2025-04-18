﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Tabeekh.DTOs;
using Tabeekh.Models;

namespace Tabeekh.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(TabeekhDBContext _tabeekhDB , IConfiguration config) : ControllerBase
    {
        [HttpPost("Register")]
        public IActionResult Register([FromBody]UserRegisterDTO EndUser)
        {
            EndUser userToDB = new EndUser();
            var userDB = _tabeekhDB.EndUsers.FirstOrDefault(u=>u.Email == EndUser.Email || u.Username == EndUser.Username);
            if (userDB != null) 
            { 
                return BadRequest(new { message = "Username or Email is already taken"});
            }
            
            if (EndUser == null)
            {
                return BadRequest(new { message = "Invalid user"});
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var hashedPassword = new PasswordHasher<UserRegisterDTO>().HashPassword(EndUser,EndUser.Password);

            userToDB.Email = EndUser.Email;
            userToDB.Username = EndUser.Username;
            userToDB.Phone = EndUser.Phone;
            userToDB.Role = EndUser.Role;
            userToDB.Id = EndUser.Id;
            userToDB.Password = hashedPassword;
            
            _tabeekhDB.EndUsers.Add(userToDB);
            _tabeekhDB.SaveChanges();
            return Ok(new { message = " registered successfully", userToDB });
        }
        [HttpPost("Login")]
        public IActionResult Login([FromBody] UserDTO user)
        {
            var userDB = _tabeekhDB.EndUsers.FirstOrDefault(u => u.Email == user.Email);
            
            if (userDB == null)
            {
                return BadRequest(new { message = "User not found" });
            }

            if (user == null)
            {
                return BadRequest(new { message = "Invalid user" });
            }
            var VerifyPassword = new PasswordHasher<EndUser>().VerifyHashedPassword(userDB,userDB.Password ,user.Password);
            if (VerifyPassword == PasswordVerificationResult.Failed)
            {
                return BadRequest("Invalid password");
            }
            return Ok(GetToken(userDB));
        }

        [HttpDelete("Delete")]
        public IActionResult DeleteUser([FromBody] UserDTO user)
        {
            var userDB = _tabeekhDB.EndUsers.FirstOrDefault(u => u.Email == user.Email);

            if (userDB == null)
            {
                return BadRequest(new { message = "User not found" });
            }
            var VerifyPassword = new PasswordHasher<EndUser>().VerifyHashedPassword(userDB, userDB.Password, user.Password);
            if (VerifyPassword == PasswordVerificationResult.Success)
            {
                _tabeekhDB.EndUsers.Remove(userDB);
                _tabeekhDB.SaveChanges();
                return Ok(string.Format("user: {0} has been removed successfully", user.Email));
            }

            return BadRequest("Invalid user name or email");
        }

        [HttpGet("GetUsers")]
        public IActionResult GetUsers()
        {
            var users = _tabeekhDB.EndUsers.ToList();
            if (users == null || !users.Any())
            {
                return NotFound("No users found.");
            }
            return Ok(users);
        }
        [HttpPost("Change-password")]
        public IActionResult ChangePassword([FromBody] UserDTO user, string newPassword)
        {

           
            var userDB = _tabeekhDB.EndUsers.FirstOrDefault(u=>u.Email == user.Email);
            if (userDB == null )
            {
                return NotFound("No user found");
            }
            var verifyPassword = new PasswordHasher<EndUser>().VerifyHashedPassword(userDB,userDB.Password,user.Password);
            if (verifyPassword == PasswordVerificationResult.Failed)
            {
                return NotFound("Invalid email or password");
            }

            var hashPassword = new PasswordHasher<EndUser>().HashPassword(userDB, newPassword);
            userDB.Password = hashPassword;
            _tabeekhDB.Update(userDB);
            _tabeekhDB.SaveChanges();
            return Ok("Password has been changed successfully");
        }
        private string GetToken(EndUser user)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name,user.Username),
                new Claim(ClaimTypes.Role,user.Role.ToString())

            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetValue<string>("Application:Token")!));

            var token = new JwtSecurityToken(
                issuer: config.GetValue<string>("Application:Issuer"),
                audience: config.GetValue<string>("Application:Audience"),
                claims: claims,
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256),
                expires: DateTime.Now.AddHours(2));
                
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
