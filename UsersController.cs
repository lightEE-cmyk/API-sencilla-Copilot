using Microsoft.AspNetCore.Mvc;
using UserManagementAPI.Models;
using Microsoft.AspNetCore.Authorization;

namespace UserManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private static List<User> _users = new List<User>
        {
            new User { Id = 1, Name = "Juan Perez", Email = "juan@techhive.com" }
        };

        // Paso 3, Actividad 1: GET Usuarios
        [HttpGet]
        public IActionResult GetAll() => Ok(_users);

        // Paso 2, Actividad 2: Manejo de errores en búsquedas fallidas
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);
            if (user == null) return NotFound(new { message = "Usuario no encontrado" });
            return Ok(user);
        }

        // Paso 3, Actividad 1 & Paso 3 Actividad 2: POST con Validación
        [HttpPost]
        public IActionResult Create(User user)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            
            user.Id = _users.Any() ? _users.Max(u => u.Id) + 1 : 1;
            _users.Add(user);
            return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, User updatedUser)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);
            if (user == null) return NotFound();
            
            if (!ModelState.IsValid) return BadRequest(ModelState);

            user.Name = updatedUser.Name;
            user.Email = updatedUser.Email;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);
            if (user == null) return NotFound();
            
            _users.Remove(user);
            return NoContent();
        }
    }
}
