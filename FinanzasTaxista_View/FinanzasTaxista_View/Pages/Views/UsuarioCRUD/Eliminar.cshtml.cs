using FinanzasTaxista_View.Models;
using FinanzasTaxista_View.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FinanzasTaxista_View.Pages.Views.UsuarioCRUD
{
    [Authorize(Roles = "Administrador")]
    public class EliminarModel : PageModel
    {
        private readonly UsuarioService _usuarioService;

        public EliminarModel(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [BindProperty]

        public UsuarioModel _usuarioModel { get; set; } = new();

        public string message { get; set; } = string.Empty;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var bills = await _usuarioService.GetUsuariosAsync();
            _usuarioModel = bills.FirstOrDefault(u => u.id == id);
            if (_usuarioModel == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<ActionResult> OnPostAsync()
        {
            var response = await _usuarioService.DeleteUsuarioAsync(_usuarioModel.id);
            if (response)
            {
                return RedirectToPage("/Views/UsuariosView");
            }
            else
            {
                message = "Error al eliminar el usuario";
                return Page();
            }
        }

    }
}
