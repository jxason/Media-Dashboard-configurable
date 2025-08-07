using FinanzasTaxista_Api.DBContext;
using FinanzasTaxista_Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace FinanzasTaxista_Api.Controllers
{
    
    [Route("api/[controller]")]
    public class ViajeController : Controller
    {
        private readonly ApplicationDBContext _context;

        // Constructor
        public ViajeController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: api/Viaje

        [HttpGet]
        public async Task<ActionResult<List<Viaje>>> GetViajes()
        {

            return await _context.viaje.ToListAsync();

        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Viaje>> GetViajeById(int id)
        {
            var viaje = await _context.viaje.FindAsync(id);

            if (viaje == null)
            {
                return NotFound();
            }

            return viaje;
        
        }

        [HttpPost]
        public async Task<ActionResult<Viaje>> AddViaje([FromBody] Viaje viaje)
        {
            if (!ModelState.IsValid)
            {
                var mensajeError = new { msg = "El modelo no se carga correctamente." };
                return BadRequest(mensajeError);
            }

            var mensajeCorrecto = new { msg = "Viaje añadido correctamente." };
            _context.viaje.Add(viaje);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetViajeById), new { id = viaje.id }, new { viaje = viaje, mensajeCorrecto });

        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Viaje>> UpdateViaje(int id, [FromBody] Viaje viaje)
        {
            if (id != viaje.id)
            {
                return BadRequest(new { msg = "El id proporcionado no coincide con el viaje." });
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.viaje.Update(viaje);
            await _context.SaveChangesAsync();
            return Ok();

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteViaje(int id)
        {
            var viaje = await _context.viaje.FindAsync(id);
            if (viaje == null)
            {
                return NotFound();
            }

            _context.viaje.Remove(viaje);
            await _context.SaveChangesAsync();

            return NoContent();

        }

    }
}
