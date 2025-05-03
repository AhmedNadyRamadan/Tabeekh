using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NuGet.Protocol;
using Tabeekh.DTOs;
using Tabeekh.Models;

namespace Tabeekh.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(TabeekhDBContext _tabeekhDB , IConfiguration config) : ControllerBase
    {
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody]UserRegisterDTO EndUser)
        {
            EndUser userToDB = new EndUser();
            Chief Chief = new Chief();
            Customer Customer = new Customer();

            var userDB = await _tabeekhDB.EndUsers.FirstOrDefaultAsync(u=>u.Email == EndUser.Email || u.Name == EndUser.Username);
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
            userToDB.Name = EndUser.Username;
            userToDB.Phone = EndUser.Phone;
            userToDB.Role = EndUser.Role;
            userToDB.Id = EndUser.Id;
            userToDB.Address = EndUser.Address;
            userToDB.Password = hashedPassword;
            userToDB.Photo = EndUser.Photo;

            if( EndUser.Role == UserType.Chief){
            Chief.Id = EndUser.Id;
            Chief.Email = EndUser.Email;
            Chief.Name = EndUser.Username;
            Chief.Phone = EndUser.Phone;
            Chief.Address = EndUser.Address;
            _tabeekhDB.Chiefs.Add(Chief);
            }

            if(EndUser.Role == UserType.Customer){
            Customer.Id = EndUser.Id;
            Customer.Email = EndUser.Email;
            Customer.Name = EndUser.Username;
            Customer.Phone = EndUser.Phone;
            Customer.Address = EndUser.Address;
            _tabeekhDB.Customers.Add(Customer);
            }

            _tabeekhDB.EndUsers.Add(userToDB);
            _tabeekhDB.SaveChanges();
            return Ok(new { message = " registered successfully", userToDB });
        }

         [HttpPut("UpdateUser/{Id:guid}")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDTO user,Guid Id)
        {
            var userDB = await _tabeekhDB.EndUsers.FirstOrDefaultAsync(u => u.Id == Id);
            if (userDB == null)
            {
                return BadRequest("Invalid user");
            }

            userDB.Address = user.Address;
            userDB.Name = user.Username;
            userDB.Email = user.Email;
            userDB.Phone = user.Phone;
            userDB.Photo = user.Photo;
            _tabeekhDB.Update(userDB);

            if (userDB.Role == UserType.Chief)
            {
                var Chief = await _tabeekhDB.Chiefs.FirstOrDefaultAsync(c=>c.Id == Id);
                if (Chief != null)
                    {    
                        Chief.Email = user.Email;
                        Chief.Name = user.Username;
                        Chief.Phone = user.Phone;
                        Chief.Address = user.Address;
                        _tabeekhDB.Update(Chief);
                    }
            }
            if (userDB.Role == UserType.Customer)
            {
                var Customer = await _tabeekhDB.Customers.FirstOrDefaultAsync(c=>c.Id == Id);
                if (Customer != null)
                    {    
                        Customer.Email = user.Email;
                        Customer.Name = user.Username;
                        Customer.Phone = user.Phone;
                        Customer.Address = user.Address;
                        _tabeekhDB.Update(Customer);
                    }
            }
            await _tabeekhDB.SaveChangesAsync();

            return Ok(new{message = $"user with id: {Id} has been updated successfully"});            
        }


        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserDTO user)
        {
            var userDB = await _tabeekhDB.EndUsers.FirstOrDefaultAsync(u => u.Email == user.Email);
            
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
            var token = GetToken(userDB);
            Response.Cookies.Append("X-Access-Token", token, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict });
            
            
            return Ok(new {token});
        }

        [HttpGet("Logout")]
        public async Task<IActionResult> Logout()
        {       
            Response.Cookies.Append("X-Access-Token", "", new CookieOptions
            {
                Expires = DateTimeOffset.UtcNow.AddDays(-1),
                HttpOnly = true,
                SameSite = SameSiteMode.Strict
            });

            return Ok("You logged out successfully");
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteUser([FromBody] UserDTO user)
        {
            var userDB = await _tabeekhDB.EndUsers.FirstOrDefaultAsync(u => u.Email == user.Email);

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

        [HttpGet("User")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _tabeekhDB.EndUsers.ToListAsync();
            if (users == null || !users.Any())
            {
                return NotFound("No users found.");
            }
            return Ok(users);
        }

        [HttpGet("User/{id:guid}")]
        public async Task<IActionResult> GetUsers(Guid id)
        {
            var user = await _tabeekhDB.EndUsers.FirstOrDefaultAsync(u=>u.Id == id);
            if (user == null )
            {
                return NotFound("No users found.");
            }
            return Ok(user);
        }
        [HttpPost("Change-password")]
        public async Task<IActionResult> ChangePassword(
            [FromBody] ChangePassDTO user)
        {

           
            var userDB = await _tabeekhDB.EndUsers.FirstOrDefaultAsync(u=>u.Email == user.email);
            if (userDB == null )
            {
                return NotFound("No user found");
            }
            var verifyPassword = new PasswordHasher<EndUser>().VerifyHashedPassword(userDB,userDB.Password,user.password);
            if (verifyPassword == PasswordVerificationResult.Failed)
            {
                return NotFound("Invalid email or password");
            }

            var hashPassword = new PasswordHasher<EndUser>().HashPassword(userDB, user.newPassword);
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
                new Claim(ClaimTypes.Name,user.Name),
                new Claim(ClaimTypes.Role,user.Role.ToString()),
                new Claim(ClaimTypes.StreetAddress,user.Address.ToString()),

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
