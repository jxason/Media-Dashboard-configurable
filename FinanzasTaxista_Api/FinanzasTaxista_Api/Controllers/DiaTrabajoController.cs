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

    }

}