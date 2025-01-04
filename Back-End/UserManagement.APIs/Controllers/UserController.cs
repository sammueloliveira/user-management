using Microsoft.AspNetCore.Mvc;
using UserManagement.Domain.Entities;
using UserManagement.Domain.Interfaces;
using UserManagement.Domain.ServicesInterfaces;

namespace UserManagement.APIs.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IUser _user;

        public UserController(IUserService userService, IUser user)
        {
            _userService = userService;
            _user = user;
        }

       
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _user.List();
            return Ok(users);
        }

     
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _user.GetEntityById(id);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

       
        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] User user)
        {
            if (user == null)
                return BadRequest("User is null.");

            await _userService.AddUser(user);
            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
        }

       
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] User user)
        {
            if (id != user.Id)
                return BadRequest("User ID mismatch.");

           
            await _userService.UpdateUser(user);
            return Ok(user);
        }

       
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _user.GetEntityById(id);

            if (user == null)
                return NotFound();

            await _user.Delete(user);
            return Ok("User deleted successfully");
        }

  
        [HttpPost("fetch-random/{count}")]
        public async Task<IActionResult> FetchAndSaveRandomUsers(int count)
        {
            var users = await _userService.FetchAndSaveRandomUsers(count);
            return Ok(users);
        }
    }
}
