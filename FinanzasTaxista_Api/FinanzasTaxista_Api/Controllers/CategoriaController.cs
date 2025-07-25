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

    }

}
