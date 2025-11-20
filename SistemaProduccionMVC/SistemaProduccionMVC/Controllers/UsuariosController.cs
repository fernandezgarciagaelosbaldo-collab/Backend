using Microsoft.AspNetCore.Mvc;
using SistemaProduccionMVC.Models;

namespace SistemaProduccionMVC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly SistemaProduccionContext _context;

        public UsuariosController(SistemaProduccionContext context)
        {
            _context = context;
        }

        // GET api/usuarios
        [HttpGet]
        public IActionResult Get() => Ok(_context.Usuarios.ToList());

        // GET api/usuarios/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var user = _context.Usuarios.Find(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        // POST api/usuarios
        [HttpPost]
        public IActionResult Post([FromBody] Usuario user)
        {
            _context.Usuarios.Add(user);
            _context.SaveChanges();
            return Ok(user);
        }

        // PUT api/usuarios/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Usuario user)
        {
            var dbUser = _context.Usuarios.Find(id);
            if (dbUser == null) return NotFound();

            dbUser.Nombre = user.Nombre;
            dbUser.Correo = user.Correo;
            dbUser.RolId = user.RolId;

            _context.SaveChanges();
            return Ok(dbUser);
        }

        // DELETE api/usuarios/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var dbUser = _context.Usuarios.Find(id);
            if (dbUser == null) return NotFound();

            _context.Usuarios.Remove(dbUser);
            _context.SaveChanges();

            return Ok(new { message = "Usuario eliminado" });
        }
    }
}
