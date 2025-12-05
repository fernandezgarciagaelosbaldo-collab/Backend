using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaProduccionMVC.Models;
using SistemaProduccionMVC.Models.Produccion;

namespace SistemaProduccionMVC.Controllers
{
    [Route("produccion")]
    [ApiController]
    public class ProduccionController : ControllerBase
    {
        private readonly ProduccionDbContext _context;

        public ProduccionController(ProduccionDbContext context)
        {
            _context = context;
        }

        // =====================================================
        // GET: /produccion/ordenes
        // =====================================================
        [HttpGet("ordenes")]
        public IActionResult GetOrdenes()
        {
            try
            {
                var lista = _context.OrdenesProduccions
                    .Select(o => new OrdenResponse
                    {
                        Id = o.IdOrden,
                        Codigo = o.Codigo,
                        Descripcion = o.Descripcion,
                        Estado = o.Estado
                    })
                    .ToList();

                return Ok(lista);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Error al obtener órdenes", detalle = ex.Message });
            }
        }

        // =====================================================
        // GET: /produccion/ordenes/{id}
        // =====================================================
        [HttpGet("ordenes/{id}")]
        public IActionResult GetOrden(int id)
        {
            try
            {
                var orden = _context.OrdenesProduccions
                    .Where(o => o.IdOrden == id)
                    .Select(o => new OrdenResponse
                    {
                        Id = o.IdOrden,
                        Codigo = o.Codigo,
                        Descripcion = o.Descripcion,
                        Estado = o.Estado
                    })
                    .FirstOrDefault();

                if (orden == null)
                    return NotFound(new { error = "Orden no encontrada" });

                return Ok(orden);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Error al buscar la orden", detalle = ex.Message });
            }
        }

        // =====================================================
        // POST: /produccion/ordenes
        // =====================================================
        [HttpPost("ordenes")]
        public IActionResult CrearOrden([FromBody] OrdenCreateRequest request)
        {
            try
            {
                // ✅ Validar DTO con DataAnnotations
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var nueva = new OrdenesProduccion
                {
                    Codigo = request.Codigo,
                    Descripcion = request.Descripcion,
                    Estado = request.Estado ?? "Pendiente"
                };

                _context.OrdenesProduccions.Add(nueva);
                _context.SaveChanges();

                return Ok(new
                {
                    message = "Orden creada correctamente",
                    id = nueva.IdOrden
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Error al crear la orden", detalle = ex.Message });
            }
        }


        // =====================================================
        // PUT: /produccion/ordenes/{id}
        // =====================================================
        [HttpPut("ordenes/{id}")]
        public IActionResult ActualizarOrden(int id, [FromBody] OrdenUpdateRequest request)
        {
            try
            {
                // ✅ Validar DTO con DataAnnotations
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var orden = _context.OrdenesProduccions.FirstOrDefault(o => o.IdOrden == id);

                if (orden == null)
                    return NotFound(new { error = "Orden no encontrada" });

                orden.Codigo = request.Codigo;
                orden.Descripcion = request.Descripcion;
                orden.Estado = request.Estado ?? "Pendiente";

                _context.SaveChanges();

                return Ok(new { message = "Orden actualizada correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Error al actualizar la orden", detalle = ex.Message });
            }
        }


        // =====================================================
        // DELETE: /produccion/ordenes/{id}
        // =====================================================
        [HttpDelete("ordenes/{id}")]
        public IActionResult EliminarOrden(int id)
        {
            try
            {
                var orden = _context.OrdenesProduccions.FirstOrDefault(o => o.IdOrden == id);

                if (orden == null)
                    return NotFound(new { error = "Orden no encontrada" });

                _context.OrdenesProduccions.Remove(orden);
                _context.SaveChanges();

                return Ok(new { message = "Orden eliminada correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Error al eliminar la orden", detalle = ex.Message });
            }
        }
    }
}