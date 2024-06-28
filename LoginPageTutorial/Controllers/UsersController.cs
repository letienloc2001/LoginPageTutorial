using LoginPageTutorial.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace LoginPageTutorial.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly MyDbContext dbContext;
        public UsersController(MyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost]
        [Route("Registration")]
        public IActionResult Registration(UserDTO userDTO)
        {
            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }

            var objUser = dbContext.Users.FirstOrDefault(x => x.Email == userDTO.Email);
            if (objUser == null)
            {
                dbContext.Users.Add(new User
                {
                    FirstName = userDTO.FirstName,
                    LastName = userDTO.LastName,
                    Email = userDTO.Email,
                    Password = userDTO.Password
                });

                dbContext.SaveChanges();
                return Ok("User registered successfully");
            }
            else
            {
                return BadRequest("User already exists with the same email address.");
            }
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login(LoginDTO loginDTO)
        {
            var user = dbContext.Users.FirstOrDefault(x => x.Email == loginDTO.Email && x.Password == loginDTO.Password);
            if (user == null)
                return Ok(user);
                // return Ok("Login successfully with email " + loginDTO.Email);
            
            return BadRequest("Login failed");
            // return View();
        }

        [HttpGet]
        [Route("GetUsers")]
        public IActionResult GetUsers()
        {
            return Ok(dbContext.Users.ToList());
        }

        [HttpGet]
        [Route("GetUser")]
        public IActionResult GetUser(int id)
        { 
            var user = dbContext.Users.FirstOrDefault(x => x.UserId == id);
            if (user != null) 
                return Ok(user);
                // return Ok("User " + user.UserId + "\n- Name: " + user.FirstName + " " + user.LastName + "\n- Email: " + user.Email + "\n- Created on: " + user.CreatedOn);
            else 
                return NoContent();
        }
    }
}
