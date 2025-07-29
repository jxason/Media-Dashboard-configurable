using FinanzasTaxista_View.Models;
using FinanzasTaxista_View.Service;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace FinanzasTaxista_View.Pages.Account
{
    public class IniciarSesionModel : PageModel
    {
        private readonly UsuarioService _usuarioService;
        private readonly RolService _rolService;

        public IniciarSesionModel(UsuarioService usuarioService, RolService rolService)
        {
            _usuarioService = usuarioService;
            _rolService = rolService;
        }

        [BindProperty]
        public UsuarioModel usuarioModel { get; set; } = new();

        public string Mensaje { get; set; } = string.Empty;

        public async Task<IActionResult> OnPostAsync()
        {
            // Validar que el email y la contraseña no estén vacíos
            if (string.IsNullOrWhiteSpace(usuarioModel.correo_electronico) || string.IsNullOrWhiteSpace(usuarioModel.contrasena))
            {
                Mensaje = "El correo electrónico y la contraseña son obligatorios.";
                return Page();
            }

            // Consultar usuarios
            var usuarios = await _usuarioService.GetUsuariosAsync();
            var usuarioValido = usuarios.FirstOrDefault(u =>
                u.correo_electronico == usuarioModel.correo_electronico &&
                u.contrasena == usuarioModel.contrasena);

            if (usuarioValido == null)
            {
                Mensaje = "Credenciales incorrectas. Por favor, inténtalo de nuevo.";
                return Page();
            }

            // Obtener el rol basado en role_id
            var rol = await _rolService.GetRolByIdAsync(usuarioValido.id_rol);

            if (rol == null || string.IsNullOrWhiteSpace(rol.nombre_rol))
            {
                Mensaje = "El rol del usuario no es válido.";
                return Page();
            }

            // Configurar las claims con el nombre del rol
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, usuarioValido.correo_electronico),
                
                new Claim(ClaimTypes.Role, rol.nombre_rol), // Asignar el rol al claim.
                
                new Claim("UserId", usuarioValido.id.ToString()) // Capturamos el rol de usuario que se inicia sesión.

            };

            var identity = new ClaimsIdentity(claims, "YourScheme");

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity));


            return RedirectToPage("/Views/UsuariosView");




        }

    }

}
