
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FinanzasTaxista_Api.DBContext;
using FinanzasTaxista_Api.Models;
using Microsoft.AspNetCore.Http.HttpResults;
namespace FinanzasTaxista_Api.Controllers
{

    [Route("api/[controller]")]
    public class UsuariosController
    {
        private readonly AplicationDBContext _context;


        // Constructor\
        public UsuariosController(AplicationDBContext context)
        {
            _context = context;
        }

        // GET: api/Usuarios

        [HttpGet]
        public async Task<ActionResult<List<Usuarios>>> GetUsuarios()
        {
          return await _context.usuario.ToListAsync();

        }


    }
}
