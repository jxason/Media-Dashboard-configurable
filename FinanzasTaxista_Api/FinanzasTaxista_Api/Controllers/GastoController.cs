using FinanzasTaxista_Api.DBContext;
using FinanzasTaxista_Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinanzasTaxista_Api.Controllers
{

    [Route("api/[controller]")]
    public class GastoController : Controller
    {
        private readonly ApplicationDBContext _context;

        // Constructor
        public GastoController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: api/Gasto

        [HttpGet]
        public async Task<ActionResult<List<Gasto>>> GetGastos()
        {

            return await _context.gasto.ToListAsync();

        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Gasto>> GetGastoById(int id)
        {
            var gasto = await _context.gasto.FindAsync(id);

            if (gasto == null)
            {
                return NotFound();
            }

            return gasto;

        }

        [HttpPost]
        public async Task<ActionResult<Gasto>> AddGasto([FromBody] Gasto gasto)
        {
            if (!ModelState.IsValid)
            {
                var mensajeError = new { msg = "El modelo no se carga correctamente." };
                return BadRequest(mensajeError);
            }

            var mensajeCorrecto = new { msg = "Gasto añadido correctamente." };
            _context.gasto.Add(gasto);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetGastoById), new { id = gasto.id }, new { data = gasto, mensajeCorrecto });

        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Gasto>> UpdateGasto(int id, [FromBody] Gasto gasto)
        {
            if (id != gasto.id)
            {
                return BadRequest(new { msg = "El id proporcionado no coincide con el gasto." });
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.gasto.Update(gasto);
            await _context.SaveChangesAsync();
            return Ok();

        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGasto(int id)
        {
            var gasto = await _context.gasto.FindAsync(id);
            if (gasto == null)
            {
                return NotFound();
            }

            _context.gasto.Remove(gasto);
            await _context.SaveChangesAsync();

            return NoContent();

        }

    }
}