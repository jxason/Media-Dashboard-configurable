using FinanzasTaxista_View.Models;
using FinanzasTaxista_View.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FinanzasTaxista_View.Pages.Views.Account
{
    public class RegistroModel : PageModel
    {
        private readonly UsuarioService _usuarioService;

        public RegistroModel(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [BindProperty]
        public UsuarioModel _usuarioModel { get; set; } = new();

        public string message { get; set; } = string.Empty;

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    message = "Datos inválidos.";
                    return Page();
                }

                var response = await _usuarioService.AddUsuarioAsync(_usuarioModel);
                if (response)
                {
                    message = "Usuario registrado correctamente.";
                    return RedirectToPage("/Views/Account/Login");
                }
                else
                {
                    message = "Error al registrar el usuario.";
                    return Page();
                }
            }
            catch (Exception ex)
            {
                message = $"Ocurrió un error: {ex.Message}";
                return Page();
            }
        }
    }
}

