using E_CommerceSystem_API.DTOs;
using E_CommerceSystem_API.Models;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;

namespace E_CommerceSystem_API.Controllers
{


    [ApiController]
    [Route("api/User")]
    public class UserController: ControllerBase
    {
        ApplicationDbContext context= new ApplicationDbContext();

        [HttpPost("RegisterUser")]
        public IActionResult RegisterUser(UserRegisterDTO userDto)
        {
            User u = new User();
            u.Name = userDto.Name;
            u.Email = userDto.Email;
            u.Phone = userDto.Phone;
            u.Password = userDto.Password;
            u.Role = "User";
            u.CreatedAt = DateTime.Now;

            context.Users.Add(u);
            context.SaveChanges();

            return Ok("User registered succefully with ID = " + u.UserId);

        }




        [HttpPost("Login")]
        public IActionResult Login(string Email, string Password)
        {
            var user = context.Users.FirstOrDefault(u =>u.Email == Email&& u.Password == Password);

            if (user == null)
            {
                return BadRequest("Invalid email or password");
            }

            return Ok(user);
        }




        [HttpGet("GetUserById")]
        public IActionResult GetUserById(int id)
        {
            var user = context.Users.FirstOrDefault(u => u.UserId == id);

            if (user == null)
            {
                return NotFound("User not found");
            }

            var OutPutUsers = new List<UserOutputDTO>();

            OutPutUsers.Add(new UserOutputDTO
            {
                Name = user.Name,
                Email = user.Email,
                Phone = user.Phone
            });

            return Ok(OutPutUsers);
        }





    }
}
