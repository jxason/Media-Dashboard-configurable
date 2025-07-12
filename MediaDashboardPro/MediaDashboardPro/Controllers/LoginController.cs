using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using MediaDashboardPro.Models;
using MediaDashboardPro.Data;

namespace MediaDashboardPro.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApplicationDbContext _db;

        public LoginController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpPost]
        public async Task<IActionResult> SigIn(User user)
        {
            // Validar campos obligatorios.
            if (string.IsNullOrWhiteSpace(user.nombre_usuario) || string.IsNullOrWhiteSpace(user.password))
            {
                ViewBag.Mensaje = "Se requiere el nombre de usuario y contraseña.";
                return RedirectToAction("Login", "Home");
            }

            // Buscar usuario con credenciales válidas.
            var validUser = await _db.Usuario
                .FirstOrDefaultAsync(u =>
                    u.nombre_usuario == user.nombre_usuario &&
                    u.password == user.password);

            if (validUser == null)
            {
                ViewBag.Mensaje = "Credenciales incorrectas.";
                return RedirectToAction("Login", "Home");
            }

            // Buscar rol asociado.
            var role = await _db.Rol.FirstOrDefaultAsync(r => r.id == validUser.rol_id);

            if (role == null || string.IsNullOrWhiteSpace(role.nombre_rol))
            {
                ViewBag.Mensaje = "El rol del usuario no es válido.";
                return RedirectToAction("Login", "Home");
            }

            // Crear una lista de "claims" (información sobre el usuario) que se almacenará en la cookie de autenticación.
            var claims = new List<Claim>
            {
                // Agrega el nombre de usuario como un claim.
                new Claim(ClaimTypes.Name, validUser.nombre_usuario),
                
                // Agrega el nombre de rol como un claim.
                new Claim(ClaimTypes.Role, role.nombre_rol),
                
                //new Claim("UserId", validUser.id.ToString())
            };


            // Crea una identidad basada en los claims anteriores usando el esquema de autenticación por cookies.
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            // Inicia sesión al usuario creando una cookie de autenticación con la identidad generada.
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity));

            // Redirigir después del login.
            return RedirectToAction("Index", "Home");

        }
    }
}

