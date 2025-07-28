using FinanzasTaxista_Api.DBContext;
using FinanzasTaxista_Api.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

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

        [HttpGet("{id}")]
        public async Task<ActionResult<Rol>> GetRolById(int id)
        {
            var rol = await _context.rol.FindAsync(id);

            if (rol == null)
            {
                return NotFound();
            }

            return rol;
        }

        [HttpPost]
        public async Task<ActionResult<Rol>> AddRol([FromBody] Rol rol)
        {
            if (!ModelState.IsValid)
            {
                var mensajeError = new { msg = "El modelo no se carga correctamente." };
                return BadRequest(mensajeError);
            }

            var mensajeCorrecto = new { msg = "Rol añadido correctamente." };
            _context.rol.Add(rol);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRolById), new { id = rol.id }, new { rol = rol, mensajeCorrecto });

        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Rol>> UpdateRol(int id, [FromBody] Rol rol)
        {
            if (id != rol.id)
            {
                return BadRequest(new { msg = "El id proporcionado no coincide con el rol." });
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.rol.Update(rol);
            await _context.SaveChangesAsync();
            return Ok();

        }
    
    }
}
