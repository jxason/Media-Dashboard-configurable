using FinanzasTaxista_View.Models.DTO_s;
using FinanzasTaxista_View.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FinanzasTaxista_View.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly UsuarioService _usuariosService;

        public RegisterModel(UsuarioService usuariosService)
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

            if (resultado)
            {
                message = "Usuario registrado con éxito.";
                return RedirectToPage("/Views/CategoriasView/");
            }

            message = "Error al registrar usuario. Verifique los datos.";
            return Page();
        }
    }
}
