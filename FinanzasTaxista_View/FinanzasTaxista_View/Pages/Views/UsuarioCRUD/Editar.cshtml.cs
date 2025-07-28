using FinanzasTaxista_View.Models;
using FinanzasTaxista_View.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace FinanzasTaxista_View.Pages.Views.UsuarioCRUD
{
    public class EditarModel : PageModel
    {
        private readonly UsuarioService _usuarioService;

        public EditarModel(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [BindProperty]
        public UsuarioModel _usuarioModel { get; set; } = new();

        public string message { get; set; } = string.Empty;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var usuario = await _usuarioService.GetUsuariosAsync();
            
            _usuarioModel = usuario.FirstOrDefault(u => u.id == id);
            
            if (_usuarioModel == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    message = "Datos inv�lidos.";
                    return Page();
                }

                // Verificar que el ID est� presente
                if (_usuarioModel.id <= 0)
                {
                    message = "id de usuario inv�lido.";
                    return Page();
                }

                var response = await _usuarioService.UpdateUsuarioAsync(_usuarioModel);
                if (response)
                {
                    message = "Usuario actualizado.";
                    return RedirectToPage("/Views/UsuariosView");
                }
                else
                {
                    message = "Error al actualizar el usuario.";
                    return Page();
                }
            }
            catch (Exception ex)
            {
                message = $"Error: {ex.Message}";
                return Page();
            }
        }
    
    }
}
