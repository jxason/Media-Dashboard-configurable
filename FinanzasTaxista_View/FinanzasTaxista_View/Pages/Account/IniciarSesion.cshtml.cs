using FinanzasTaxista_View.DTO_s;
using FinanzasTaxista_View.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FinanzasTaxista_View.Pages.Account

{
    public class LoginModel : PageModel
    {
        private readonly UsuarioService _usuarioService;

        public LoginModel(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [BindProperty]
        public UsuarioLoginDTO usuarioLogin { get; set; }

        public string Mensaje { get; set; } = "";

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var usuario = await _usuarioService.LoginAsync(usuarioLogin);

            if (usuario != null)
            {
                // Aqu� podr�as guardar el usuario en sesi�n si quisieras
                return RedirectToPage("/Privacy");
            }

            Mensaje = "Usuario o contrase�a incorrectos";
            return Page();
        }
    }
}
