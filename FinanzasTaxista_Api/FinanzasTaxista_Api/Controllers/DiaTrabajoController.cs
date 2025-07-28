using FinanzasTaxista_Api.DBContext;
using FinanzasTaxista_Api.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace FinanzasTaxista_Api.Controllers
{

    [Route("api/[controller]")]
    public class DiaTrabajoController : Controller
    {
        private readonly ApplicationDBContext _context;


        // Constructor
        public DiaTrabajoController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: api/DiaTrabajo

        [HttpGet]
        public async Task<ActionResult<List<DiaTrabajo>>> GetDiasTrabajo()
        {

            return await _context.dia_trabajo.ToListAsync();

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DiaTrabajo>> GetDiaTrabajoById(int id)
        {
            var diaTrabajo = await _context.dia_trabajo.FindAsync(id);

            if (diaTrabajo == null)
            {
                return NotFound();
            }

            return diaTrabajo;
        }

        [HttpPost]
        public async Task<ActionResult<Rol>> AddDiaTrabajo([FromBody] DiaTrabajo diaTrabajo)
        {
            if (!ModelState.IsValid)
            {
                var mensajeError = new { msg = "El modelo no se carga correctamente." };
                return BadRequest(mensajeError);
            }

            var mensajeCorrecto = new { msg = "Día de trabajo añadido correctamente." };
            _context.dia_trabajo.Add(diaTrabajo);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDiaTrabajoById), new { id = diaTrabajo.id }, new { workDay = diaTrabajo, mensajeCorrecto });

        }


        [HttpPut("{id}")]
        public async Task<ActionResult<DiaTrabajo>> UpdateDiaTrabajo(int id, [FromBody] DiaTrabajo dia)
        {
            if (id != dia.id)
            {
                return BadRequest(new { msg = "El id proporcionado no coincide con el dia de trabajo." });
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.dia_trabajo.Update(dia);
            await _context.SaveChangesAsync();
            return Ok();

        }


        // Eliminar dia de trabajo.
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDiaTrabajo(int id)
        {
            var dia = await _context.dia_trabajo.FindAsync(id);
            if (dia == null)
            {
                return NotFound();
            }

            _context.dia_trabajo.Remove(dia);
            await _context.SaveChangesAsync();

            return NoContent();

        }
    }

}

