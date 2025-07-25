using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FinanzasTaxista_Api.DBContext;
using FinanzasTaxista_Api.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace FinanzasTaxista_Api.Controllers
{

    [Route("api/[controller]")]
    public class RolController : Controller
    {
        private readonly ApplicationDBContext _context;


        // Constructor
        public RolController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: api/Rol

        [HttpGet]
        public async Task<ActionResult<List<Rol>>> GetRoles()
        {
            return await _context.rol.ToListAsync();

        }

        [HttpPost]
        public async Task<IActionResult> AddRol(Rol rol)
        {

            _context.rol.Add(rol);
            await _context.SaveChangesAsync();

            return Ok(rol);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRol(int id, [FromBody] Rol rol)
        {
            // Validar que el id en la ruta coincida con el ID en el cuerpo de la solicitud.
            if (id != rol.id)
            {
                return BadRequest("El ID en la ruta no coincide con el ID en el cuerpo de la solicitud.");
            }

            // Validar el modelo.
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Verificar si el producto existe en la base de datos.
            var rolExistente = await _context.rol.FindAsync(id);
            
            if (rolExistente == null)
            {
                return NotFound("El rol no existe.");
            }

            // Actualizar las propiedades del producto existente.
            _context.Entry(rolExistente).CurrentValues.SetValues(rol);

            try
            {
                // Guardar los cambios en la base de datos.
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.rol.Any(p => p.id == id))
                {
                    return NotFound("El rol no existe.");
                }
                else
                {
                    throw; // Lanzar la excepción si no se puede manejar.
                }
            }
            
            // Devolver una respuesta 204 (No Content).
            return NoContent();
        }
    }
}
