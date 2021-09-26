using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VisitsTaskWebAPI_MohammedElmorsy.Models;
using VisitsTaskWebAPI_MohammedElmorsy.Repositories;

namespace VisitsTaskWebAPI_MohammedElmorsy.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : Controller
    {
        private UserRepository userRepository;

        public UserController(UserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(User _user)
        {
            var user = userRepository.Authenticate(_user.UserName, _user.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Register(User _user)
        {
            if (userRepository.UserIsExist(_user))
                return BadRequest("user already exists");

            return Ok( userRepository.Add(_user) );
        }

        [HttpGet]
        public IActionResult ValidateToken()
        {
            return Ok(new { message = "valid token" });
        }

        [HttpGet("{id}")]
        //  [ValidateAntiForgeryToken]
        public IActionResult Logout(int id)
        {
            User userWithDeletedToken = userRepository.DeleteToken(id);
            if (userWithDeletedToken != null)
            {
                userRepository.Update(userWithDeletedToken);
                return Ok(new { message = "logged out successfully" });
            }

            return BadRequest("couldn't delete user token");
        }

        [HttpPost]
        public IActionResult SendEmail(Visitor visitor)
        {
            if (userRepository.SendEmailToVisitor(visitor))
                return Ok(new { message = "email sent successfully" });

            return BadRequest("couldn't send email");

        }



    }
}
