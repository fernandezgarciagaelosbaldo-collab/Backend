using Microsoft.AspNetCore.Mvc;
using SistemaProduccionMVC.Models;
using SistemaProduccionMVC.Models.Produccion;
using SistemaProduccionMVC.Models.Scrap;
using System;
using System.Linq;

namespace SistemaProduccionMVC.Controllers
{
    [Route("scrap")]
    [ApiController]
    public class ScrapController : ControllerBase
    {
        private readonly ProduccionDbContext _context;

        public ScrapController(ProduccionDbContext context)
        {
            _context = context;
        }

        // =====================================================
        // GET: /scrap
        // Lista todos los registros de scrap
        // =====================================================
        [HttpGet]
        public IActionResult GetScrap()
        {
            try
            {
                var lista = _context.RegistrosScraps
                    .Select(s => new ScrapResponse
                    {
                        Id = s.IdScrap,
                        FechaHora = s.FechaHora,
                        Linea = s.Linea,
                        OrdenProduccion = s.OrdenProduccion,
                        Sku = s.Sku,
                        Cantidad = s.Cantidad,
                        CostoUnitario = s.CostoUnitario,
                        CostoTotal = s.CostoTotal,
                        UsuarioReporta = s.UsuarioReporta
                    })
                    .ToList();

                return Ok(lista);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Error al obtener registros de scrap", detalle = ex.Message });
            }
        }

        // =====================================================
        // GET: /scrap/{id}
        // Obtiene un registro de scrap por id
        // =====================================================
        [HttpGet("{id}")]
        public IActionResult GetScrapById(int id)
        {
            try
            {
                var scrap = _context.RegistrosScraps
                    .Where(s => s.IdScrap == id)
                    .Select(s => new ScrapResponse
                    {
                        Id = s.IdScrap,
                        FechaHora = s.FechaHora,
                        Linea = s.Linea,
                        OrdenProduccion = s.OrdenProduccion,
                        Sku = s.Sku,
                        Cantidad = s.Cantidad,
                        CostoUnitario = s.CostoUnitario,
                        CostoTotal = s.CostoTotal,
                        UsuarioReporta = s.UsuarioReporta
                    })
                    .FirstOrDefault();

                if (scrap == null)
                    return NotFound(new { error = "Scrap no encontrado" });

                return Ok(scrap);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Error al buscar scrap", detalle = ex.Message });
            }
        }

        // =====================================================
        // POST: /scrap
        // Crea un nuevo registro de scrap
        // =====================================================
        [HttpPost]
        public IActionResult CrearScrap([FromBody] ScrapCreateRequest request)
        {
            try
            {
                // Validaciones básicas
                if (string.IsNullOrWhiteSpace(request.Linea))
                    return BadRequest(new { error = "La línea es obligatoria" });

                if (request.Cantidad <= 0)
                    return BadRequest(new { error = "La cantidad debe ser mayor a cero" });

                if (request.CostoUnitario <= 0)
                    return BadRequest(new { error = "El costo unitario debe ser mayor a cero" });

                var nuevo = new RegistrosScrap
                {
                    Linea = request.Linea,
                    OrdenProduccion = request.OrdenProduccion,
                    Sku = request.Sku,
                    Cantidad = request.Cantidad,
                    CostoUnitario = request.CostoUnitario,
                    UsuarioReporta = request.UsuarioReporta
                    // FechaHora y CostoTotal los calcula la BD (según tu OnModelCreating)
                };

                _context.RegistrosScraps.Add(nuevo);
                _context.SaveChanges();

                return Ok(new
                {
                    message = "Scrap registrado correctamente",
                    id = nuevo.IdScrap
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Error al registrar scrap", detalle = ex.Message });
            }
        }

        // =====================================================
        // DELETE: /scrap/{id}
        // Elimina un registro de scrap por id
        // =====================================================
        [HttpDelete("{id}")]
        public IActionResult EliminarScrap(int id)
        {
            try
            {
                var scrap = _context.RegistrosScraps.FirstOrDefault(s => s.IdScrap == id);

                if (scrap == null)
                    return NotFound(new { error = "Scrap no encontrado" });

                _context.RegistrosScraps.Remove(scrap);
                _context.SaveChanges();

                return Ok(new { message = "Scrap eliminado correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Error al eliminar scrap", detalle = ex.Message });
            }
        }
    }
}
