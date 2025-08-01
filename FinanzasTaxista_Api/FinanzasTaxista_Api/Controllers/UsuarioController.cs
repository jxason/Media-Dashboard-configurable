using FinanzasTaxista_Api.DBContext;
using FinanzasTaxista_Api.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FinanzasTaxista_Api.DTO_s;
using Microsoft.IdentityModel.Tokens;
using BCrypt.Net;

namespace FinanzasTaxista_Api.Controllers
{
    [ApiController]
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
        /**********************************************************************************************************************************************************************************************************************/
        // POST: api/Usuario/registrar

        [HttpPost("registrar")]
        public async Task<IActionResult> RegistrarUsuario([FromBody] UsuarioRegistroDTO dto)
        {
            // Validar si el modelo es valido
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Verificar si la contraseña esta good
            if (!ValidarPassword(dto.contrasena))
                return BadRequest("La contraseña no cumple los requisitos establecidos");

            // Verificar si el correo y usuario ya existe 
            var existecorreo = await _context.usuario.AnyAsync(u => u.correo_electronico == dto.correo_electronico);
            var existeUsuario = await _context.usuario.AnyAsync(u => u.nombre_usuario == dto.nombre_usuario);

            if (existecorreo)
                return Conflict("El correo ya está registrado.");

            if (existeUsuario)
                return Conflict("El nombre de usuario ya está registrado.");

            // Vamos a asignar rol invitado una vez registrado
            var rolInvitado = await _context.rol.FirstOrDefaultAsync(r => r.nombre_rol == "Invitado");
            if (rolInvitado == null)
                return BadRequest("No se encontró el rol Invitado en la base de datos.");

            // hashear la contraseña (usaremos BCypt instalar la dependdencia porfa es esta dotnet add package BCrypt.Net-Next
            string hash = BCrypt.Net.BCrypt.HashPassword(dto.contrasena);
            
            // Crear el objeto real del modelo
            var nuevoUsuario = new Usuario
            {
                id_rol = rolInvitado.id, // asignamos
                nombre_usuario = dto.nombre_usuario,
                apellido1 = dto.apellido1,
                apellido2 = dto.apellido2,
                correo_electronico = dto.correo_electronico,
                contrasena = hash // Guardar la contraseña hasheada
            };

            // Guardar en la base de datos
            _context.usuario.Add(nuevoUsuario);
            await _context.SaveChangesAsync();

            // Retornar éxito
            return Ok("Usuario registrado exitosamente.");
        
        }

        // POST: api/Usuario/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLogin dto)
        {
            // Validar si el modelo es valido
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Verificar si el identificador es un correo electrónico o un nombre de usuario
            var usuario = await _context.usuario
                .FirstOrDefaultAsync(u =>
                    u.correo_electronico == dto.Identificador ||
                    u.nombre_usuario == dto.Identificador);// la idea de esto es poder utilizar el nombre de usuario que esta en estado unique(demole uso jaja)

            // Verificar si el usuario existe y si la contraseña es correcta
            if (usuario == null || !BCrypt.Net.BCrypt.Verify(dto.Password, usuario.contrasena))
                return Unauthorized("Usuario/correo o contraseña inválidos");

            // Aquí mas adelante se podra generar un token JWT o usar una cookie para autenticación o usar sp's como el sp dia_trabajo

            return Ok(new
            {
                mensaje = "Login exitoso",
                usuario = new
                {
                    usuario.id,
                    usuario.nombre_usuario,
                    usuario.correo_electronico,
                    usuario.id_rol
                }
            });
            // Si todo sale good simplemente retornamos un mensaje de login exitoso y el usuario con su id, nombre, correo y rol
            // la idea es que el front pueda usar esto para mostrar el usuario logueado y su rol en la aplicacion
        }
        /**********************************************************************************************************************************************************************************************************************/


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

        //VALIDACIONES ASINCRONAS

        //Registro de usuario
        
        // 1.Vamos a validar que la contraseña este bien escrita
        private bool ValidarPassword(string pass)
        {
            if (pass.Length < 8 || pass.Length > 16)
                return false;

            int letras = pass.Count(char.IsLetter);
            int numeros = pass.Count(char.IsDigit);
            bool validacion = pass.Any(c => !char.IsLetterOrDigit(c));
  
            // Validar que tenga al menos 3 letras, 3 números y 1 símbolo
            return letras >= 3 && numeros >= 3 && validacion;
        }


    }
}
