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
        public async Task<ActionResult<List<DiaTrabajo>>> GetDias()
        {

            return await _context.dia_trabajo.ToListAsync();

        }

        // POST: api/DiaTrabajo/registrar

        [HttpPost("registrar")]
            public async Task<IActionResult> RegistrarDiaTrabajo([FromQuery] int idUsuario)
        {
                /*/ Verificamos que el usuario esté "logueado" (simulado en sesión ya que aun no tenemos login)
                int? idUsuario = HttpContext.Session.GetInt32("IdUsuario");*/ //queda en el aire por que no tenemos usuarios que identificar
                if (idUsuario == null)
                {
                    return Unauthorized(new { mensaje = "Usuario no autenticado." });
                }

                try
                {
                    // Ejecutamos el stored procedure con el idUsuario
                    await _context.Database.ExecuteSqlRawAsync("EXEC sp_RegistrarDiaTrabajo @p0", idUsuario);

                    return Ok(new { mensaje = "Día de trabajo registrado o ya existente." });
                }
                catch (DbUpdateException dbEx)
                {
                    return StatusCode(500, new { error = "Error en base de datos", detalle = dbEx.InnerException?.Message ?? dbEx.Message });
                }
                catch (System.Exception ex)
                {
                    return StatusCode(500, new { error = "Error interno del servidor", detalle = ex.Message });
                }
            }
    }

}