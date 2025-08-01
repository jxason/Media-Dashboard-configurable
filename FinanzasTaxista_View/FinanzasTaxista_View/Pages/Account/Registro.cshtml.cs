using FinanzasTaxista_View.Models.DTO_s;
using FinanzasTaxista_View.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FinanzasTaxista_View.Models;
using FinanzasTaxista_View.DTO_s;


namespace FinanzasTaxista_View.Pages.Account
{
    public class RegistroModel : PageModel
    {
        private readonly UsuarioService _usuariosService;

        public RegistroModel(UsuarioService usuariosService)
        {
            _usuariosService = usuariosService;
        }

        [BindProperty]
        public UsuarioRegisterDTO Input { get; set; }

        public string? message { get; set; }

        public void OnGet()
        {
            // Página cargada
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var resultado = await _usuariosService.RegisterAsync(Input);

            if (resultado.Exito)
            {
                message = "Usuario registrado con éxito.";
                return RedirectToPage("/Account/IniciarSesion");
            }

            message = $"Error al registrar usuario. Verifique los datos. {resultado.Mensaje}";
            return Page();
        }
    }
}
