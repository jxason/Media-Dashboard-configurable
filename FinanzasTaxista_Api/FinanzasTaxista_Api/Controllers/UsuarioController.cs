using FinanzasTaxista_Api.DBContext;
using FinanzasTaxista_Api.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Threading;

namespace FinanzasTaxista_Api.Controllers
{

    [Route("api/[controller]")]
    public class UsuarioController : Controller
    {
        private readonly ApplicationDBContext _context;


        // Constructor
        public UsuarioController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: api/Usuario

        [HttpGet]
        public async Task<ActionResult<List<Usuario>>> GetUsuarios()
        {
          
            return await _context.usuario.ToListAsync();

        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuarioById(int id)
        {
            var usuario = await _context.usuario.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return usuario;
        }

        [HttpPost]
        public async Task<ActionResult<Usuario>> AddUsuario([FromBody] Usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                var mensajeError = new { msg = "El modelo no se carga correctamente." };
                return BadRequest(mensajeError);
            }

            var mensajeCorrecto = new { msg = "Usuario añadido correctamente." };
            _context.usuario.Add(usuario);
            await _context.SaveChangesAsync();
            
            return CreatedAtAction(nameof(GetUsuarioById), new { id = usuario.id }, new { user = usuario, mensajeCorrecto });

        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Usuario>> UpdateUsuario(int id, [FromBody] Usuario usuario)
        {
            if (id != usuario.id)
            {
                return BadRequest(new { msg = "El id proporcionado no coincide con el usuario." });
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            _context.usuario.Update(usuario);
            await _context.SaveChangesAsync();
            return Ok();
        
        }


        // Eliminar usuario.
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var usuario = await _context.usuario.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            _context.usuario.Remove(usuario);
            await _context.SaveChangesAsync();

            return NoContent();
        
        }
    }
}
