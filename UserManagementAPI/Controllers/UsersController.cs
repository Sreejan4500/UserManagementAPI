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
        public IActionResult GetAll() => Ok(users);

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var user = users.FirstOrDefault(u => u.Id == id);
            return Ok(user);
        }

        [HttpPost]
        public IActionResult Create([FromBody] User newUser)
        {
            newUser.Id = users.Any() ? users.Max(u => u.Id) + 1 : 1;
            users.Add(newUser);
            return CreatedAtAction(nameof(Get), new { id = newUser.Id }, newUser);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] User updatedUser)
        {
            var user = users.FirstOrDefault(u => u.Id == id);

            user.Name = updatedUser.Name;
            user.Email = updatedUser.Email;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var user = users.FirstOrDefault(u => u.Id == id);
            users.Remove(user);
            return NoContent();
        }
    }
}
