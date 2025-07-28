using FinanzasTaxista_Api.DBContext;
using FinanzasTaxista_Api.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace FinanzasTaxista_Api.Controllers
{

    [Route("api/[controller]")]
    public class CategoriaController : Controller
    {
        private readonly ApplicationDBContext _context;


        // Constructor
        public CategoriaController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: api/Categoria

        [HttpGet]
        public async Task<ActionResult<List<Categoria>>> GetCategorias()
        {

            return await _context.categoria.ToListAsync();

        }

        [HttpPost]
        public async Task<IActionResult> AddCategoria(Categoria categoria)
        {

            _context.categoria.Add(categoria);
            await _context.SaveChangesAsync();

            return Ok(categoria);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Categoria>> UpdateCategoria(int id, [FromBody] Categoria categoria)
        {
            if (id != categoria.id)
            {
                return BadRequest(new { msg = "El id proporcionado no coincide con la categoria." });
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.categoria.Update(categoria);
            await _context.SaveChangesAsync();
            return Ok();

        }


        // Eliminar Categoria.
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoria(int id)
        {
            var categoria = await _context.categoria.FindAsync(id);
            if (categoria == null)
            {
                return NotFound();
            }

            _context.categoria.Remove(categoria);
            await _context.SaveChangesAsync();

            return NoContent();

        }

    }

}
