using FinanzasTaxista_View.DTO_s;
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
            // Validar que el email y la contrase�a no est�n vac�os  
            if (string.IsNullOrWhiteSpace(usuarioModel.correo_electronico) || string.IsNullOrWhiteSpace(usuarioModel.contrasena))
            {
                Mensaje = "El correo electr�nico y la contrase�a son obligatorios.";
                return Page();
            }

            // Consultar usuarios  
            var usuarios = await _usuarioService.GetUsuariosAsync();
            var usuarioValido = usuarios.FirstOrDefault(u =>
                u.correo_electronico == usuarioModel.correo_electronico &&
                u.contrasena == usuarioModel.contrasena);

            if (usuarioValido == null)
            {
                Mensaje = "Credenciales incorrectas. Por favor, int�ntalo de nuevo.";
                return Page();
            }

            // Obtener el rol basado en role_id  
            var rol = await _rolService.GetRolByIdAsync(usuarioValido.id_rol);

            if (rol == null || string.IsNullOrWhiteSpace(rol.nombre_rol))
            {
                Mensaje = "El rol del usuario no es v�lido.";
                return Page();
            }

            // Configurar las claims con el nombre del rol  
            var claims = new List<Claim>
               {
                   new Claim(ClaimTypes.Name, usuarioValido.correo_electronico),
                   new Claim(ClaimTypes.Role, rol.nombre_rol),
                   new Claim("UserId", usuarioValido.id.ToString())
               };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

            // Redirigir seg�n el rol  
            if (rol.nombre_rol.Equals("Taxista", StringComparison.OrdinalIgnoreCase))
            {
                return RedirectToPage("/Privacy");
            }
            else if (rol.nombre_rol.Equals("Usuario", StringComparison.OrdinalIgnoreCase))
            {
                return RedirectToPage("/Privacy");
            }
            else if (rol.nombre_rol.Equals("Administrador", StringComparison.OrdinalIgnoreCase))
            {
                return RedirectToPage("/Views/PrivacyAdmin");
            }

            // Si el rol no coincide con ninguno, redirigir a una p�gina predeterminada  
            return Page();
        }
    }

}