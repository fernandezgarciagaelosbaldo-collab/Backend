using Microsoft.AspNetCore.Mvc;
using SistemaProduccionMVC.Models;
using SistemaProduccionMVC.Models.Auth;
using System.Linq;
using BCrypt.Net;

namespace SistemaProduccionMVC.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly SistemaProduccionContext _context;

        public AuthController(SistemaProduccionContext context)
        {
            _context = context;
        }

        // login
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var user = _context.Usuarios.FirstOrDefault(u => u.Correo == request.Correo);

            if (user == null)
                return Unauthorized(new { error = "Correo no registrado" });

            // Validar contraseña con hashing
            bool contraseñaCorrecta = BCrypt.Net.BCrypt.Verify(request.Contrasena, user.Contrasena);

            if (!contraseñaCorrecta)
                return Unauthorized(new { error = "Contraseña incorrecta" });

            return Ok(new
            {
                message = "Login exitoso",
                usuario = new
                {
                    id = user.Id,
                    nombre = user.Nombre,
                    correo = user.Correo,
                    rol = user.RolId
                }
            });
        }

        // register
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterRequest request)
        {
            if (_context.Usuarios.Any(u => u.Correo == request.Correo))
                return BadRequest(new { error = "El correo ya está registrado" });

            // Hashear contraseña nueva
            string hash = BCrypt.Net.BCrypt.HashPassword(request.Contrasena);

            var nuevo = new Usuario
            {
                Nombre = request.Nombre,
                Correo = request.Correo,
                Contrasena = hash,
                RolId = request.RolId
            };

            _context.Usuarios.Add(nuevo);
            _context.SaveChanges();

            return Ok(new
            {
                message = "Usuario registrado correctamente",
                id = nuevo.Id
            });
        }

      
        // Lista de usuarios 
        [HttpGet("usuarios")]
        public IActionResult GetUsuarios()
        {
            var usuarios = _context.Usuarios
                .Select(u => new
                {
                    id = u.Id,
                    nombre = u.Nombre,
                    correo = u.Correo,
                    rol = u.RolId
                })
                .ToList();

            return Ok(usuarios);
        }

   
        // OBTENER USUARIO POR ID
  
        [HttpGet("usuarios/{id}")]
        public IActionResult GetUsuario(int id)
        {
            var user = _context.Usuarios
                .Where(u => u.Id == id)
                .Select(u => new
                {
                    id = u.Id,
                    nombre = u.Nombre,
                    correo = u.Correo,
                    rol = u.RolId
                })
                .FirstOrDefault();

            if (user == null)
                return NotFound(new { error = "Usuario no encontrado" });

            return Ok(user);
        }

        
        // ACTUALIZAR USUARIO
      
        [HttpPut("usuarios/{id}")]
        public IActionResult UpdateUsuario(int id, [FromBody] RegisterRequest request)
        {
            var user = _context.Usuarios.FirstOrDefault(u => u.Id == id);
            if (user == null)
                return NotFound(new { error = "Usuario no encontrado" });

            user.Nombre = request.Nombre;
            user.Correo = request.Correo;
            user.RolId = request.RolId;

            // Si viene contraseña, la actualizamos
            if (!string.IsNullOrWhiteSpace(request.Contrasena))
            {
                user.Contrasena = BCrypt.Net.BCrypt.HashPassword(request.Contrasena);
            }

            _context.SaveChanges();

            return Ok(new { message = "Usuario actualizado correctamente" });
        }

        //ELIMINAR USUARIO
        [HttpDelete("usuarios/{id}")]
        public IActionResult DeleteUsuario(int id)
        {
            var user = _context.Usuarios.FirstOrDefault(u => u.Id == id);
            if (user == null)
                return NotFound(new { error = "Usuario no encontrado" });

            _context.Usuarios.Remove(user);
            _context.SaveChanges();

            return Ok(new { message = "Usuario eliminado correctamente" });
        }
    }
}
