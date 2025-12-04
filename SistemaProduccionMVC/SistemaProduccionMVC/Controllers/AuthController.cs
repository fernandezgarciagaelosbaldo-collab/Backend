using Microsoft.AspNetCore.Mvc;
using SistemaProduccionMVC.Models;
using SistemaProduccionMVC.Models.Auth;
using BCrypt.Net;

namespace SistemaProduccionMVC.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ProduccionDbContext _context;

        public AuthController(ProduccionDbContext context)
        {
            _context = context;
        }

        // LOGIN
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            try
            {
                var user = _context.Usuarios.FirstOrDefault(u => u.Correo == request.Correo);

                if (user == null)
                    return Unauthorized(new { error = "Correo no registrado" });

                bool passCorrecta = BCrypt.Net.BCrypt.Verify(request.Contrasena, user.Contrasena);

                if (!passCorrecta)
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
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Error en login", detalle = ex.Message });
            }
        }

        // REGISTER
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterRequest request)
        {
            try
            {
                if (_context.Usuarios.Any(u => u.Correo == request.Correo))
                    return BadRequest(new { error = "El correo ya está registrado" });

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
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Error al registrar usuario", detalle = ex.Message });
            }
        }

        // LISTAR USUARIOS
        [HttpGet("usuarios")]
        public IActionResult GetUsuarios()
        {
            try
            {
                var lista = _context.Usuarios
                    .Select(u => new
                    {
                        id = u.Id,
                        nombre = u.Nombre,
                        correo = u.Correo,
                        rol = u.RolId
                    }).ToList();

                return Ok(lista);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Error al obtener usuarios", detalle = ex.Message });
            }
        }

        // OBTENER USUARIO POR ID
        [HttpGet("usuarios/{id}")]
        public IActionResult GetUsuario(int id)
        {
            try
            {
                var user = _context.Usuarios
                    .Where(u => u.Id == id)
                    .Select(u => new
                    {
                        id = u.Id,
                        nombre = u.Nombre,
                        correo = u.Correo,
                        rol = u.RolId
                    }).FirstOrDefault();

                if (user == null)
                    return NotFound(new { error = "Usuario no encontrado" });

                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Error en búsqueda", detalle = ex.Message });
            }
        }

        // ACTUALIZAR USUARIO
        [HttpPut("usuarios/{id}")]
        public IActionResult UpdateUsuario(int id, [FromBody] RegisterRequest request)
        {
            try
            {
                var user = _context.Usuarios.FirstOrDefault(u => u.Id == id);
                if (user == null)
                    return NotFound(new { error = "Usuario no encontrado" });

                user.Nombre = request.Nombre;
                user.Correo = request.Correo;
                user.RolId = request.RolId;

                if (!string.IsNullOrWhiteSpace(request.Contrasena))
                    user.Contrasena = BCrypt.Net.BCrypt.HashPassword(request.Contrasena);

                _context.SaveChanges();

                return Ok(new { message = "Usuario actualizado correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Error al actualizar", detalle = ex.Message });
            }
        }

        // ELIMINAR USUARIO
        [HttpDelete("usuarios/{id}")]
        public IActionResult DeleteUsuario(int id)
        {
            try
            {
                var user = _context.Usuarios.FirstOrDefault(u => u.Id == id);
                if (user == null)
                    return NotFound(new { error = "Usuario no encontrado" });

                _context.Usuarios.Remove(user);
                _context.SaveChanges();

                return Ok(new { message = "Usuario eliminado correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Error al eliminar", detalle = ex.Message });
            }
        }
    }
}
