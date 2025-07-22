using Microsoft.AspNetCore.Mvc;
using UserManagementAPI.Models;

namespace UserManagementAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private static List<User> users = new List<User>
        {
            new User { Id = 1, Name = "PersonName1", Email = "personal@email1.com" },
            new User { Id = 2, Name = "PersonName2", Email = "personal@email2.com" },
            new User { Id = 3, Name = "PersonName3", Email = "personal@email3.com" }
        };

        [HttpGet]
        public IActionResult GetAll()
        {
            try { return Ok(users); }
            catch { return StatusCode(500, "Error retrieving users."); }
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var user = users.FirstOrDefault(u => u.Id == id);
                if (user == null) return NotFound($"User with ID {id} not found.");
                return Ok(user);
            }
            catch { return StatusCode(500, "Error retrieving user."); }
        }

        [HttpPost]
        public IActionResult Create([FromBody] User newUser)
        {
            try
            {
                newUser.Id = users.Any() ? users.Max(u => u.Id) + 1 : 1;
                users.Add(newUser);
                return CreatedAtAction(nameof(Get), new { id = newUser.Id }, newUser);
            }
            catch { return StatusCode(500, "Error creating user."); }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] User updatedUser)
        {
            try
            {
                var user = users.FirstOrDefault(u => u.Id == id);
                if (user == null) return NotFound($"User with ID {id} not found.");

                user.Name = updatedUser.Name;
                user.Email = updatedUser.Email;
                return NoContent();
            }
            catch { return StatusCode(500, "Error updating user."); }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var user = users.FirstOrDefault(u => u.Id == id);
                if (user == null) return NotFound($"User with ID {id} not found.");
                users.Remove(user);
                return NoContent();
            }
            catch { return StatusCode(500, "Error deleting user."); }
        }
    }
}
