using FinanzasTaxista_View.Models;
using FinanzasTaxista_View.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace FinanzasTaxista_View.Pages.Views.Categoria
{
    [Authorize(Roles = "Administrador")]
    public class EditarModel : PageModel
    {
        private readonly CategoriaService _categoriaService;

        public EditarModel(CategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        [BindProperty]
        public CategoriaModel _categoriaModel { get; set; } = new();

        public string message { get; set; } = string.Empty;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var categoria = await _categoriaService.GetCategoriasAsync();

            _categoriaModel = categoria.FirstOrDefault(u => u.id == id);
            
            if (_categoriaModel == null)
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
                    message = "Datos inválidos.";
                    return Page();
                }

                // Verificar que el ID esté presente
                if (_categoriaModel.id <= 0)
                {
                    message = "ID de categoria inválido.";
                    return Page();
                }

                var response = await _categoriaService.UpdateCategoriaAsync(_categoriaModel);
                if (response)
                {
                    message = "Categoria actualizada.";
                    return RedirectToPage("/Views/CategoriasView");
                }
                else
                {
                    message = "Error al actualizar la categoria.";
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
