using FinanzasTaxista_View.Models;
using FinanzasTaxista_View.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FinanzasTaxista_View.Pages.Views.Categoria
{
    [Authorize(Roles = "Administrador")]
    public class CrearModel : PageModel
    {
        private readonly CategoriaService _categoriaService;

        public CrearModel(CategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        [BindProperty]
        public CategoriaModel _categoriaModel { get; set; } = new();

        public string message { get; set; } = string.Empty;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                message = "Datos inválidos.";
                return Page();
            }

            var response = await _categoriaService.AddCategoriaAsync(_categoriaModel);

            if (response)
            {
                return RedirectToPage("/Views/CategoriasView"); // vuelve a la lista
            }
            else
            {
                message = "Error al agregar la categoría.";
                return Page();
            }
        }
    }
}
