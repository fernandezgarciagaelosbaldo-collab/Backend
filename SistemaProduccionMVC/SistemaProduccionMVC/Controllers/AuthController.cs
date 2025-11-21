using Microsoft.AspNetCore.Mvc;
using SistemaProduccionMVC.Models;
using SistemaProduccionMVC.Models.Auth;
using System.Linq;

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

        // POST: Auth/login
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            // Buscar usuario por correo
            var user = _context.Usuarios
                .FirstOrDefault(u => u.Correo == request.Correo);

            if (user == null)
                return Unauthorized(new { error = "Correo no registrado" });

            // Validar contraseña simple
            if (user.Contrasena != request.Contrasena)
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

        // POST: Auth/register
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterRequest request)
        {
            // Validar duplicado
            if (_context.Usuarios.Any(u => u.Correo == request.Correo))
                return BadRequest(new { error = "El correo ya está registrado" });

            var nuevo = new Usuario
            {
                Nombre = request.Nombre,
                Correo = request.Correo,
                Contrasena = request.Contrasena,  
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
    }
}
