using FinanzasTaxista_View.Models;
using FinanzasTaxista_View.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FinanzasTaxista_View.Pages.Views.GastoCRUD
{
    public class EditarModel : PageModel
    {
        private readonly GastoService _gastoService;
        private readonly CategoriaService _categoriaService;

        public EditarModel(GastoService gastoService, CategoriaService categoriaService)
        {
            _gastoService = gastoService;
            _categoriaService = categoriaService;
        }

        [BindProperty]
        public GastoModel _gastoModel { get; set; } = new();

        public List<SelectListItem> CategoriasLista { get; set; } = new();

        public string message { get; set; } = string.Empty;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            // Cargar lista de categorías
            var categorias = await _categoriaService.GetCategoriasAsync();
            CategoriasLista = categorias.Select(c => new SelectListItem
            {
                Value = c.id.ToString(),
                Text = c.nombre_categoria
            }).ToList();

            // Cargar gasto a editar
            var gastos = await _gastoService.GetGastosAsync();
            _gastoModel = gastos.FirstOrDefault(g => g.id == id);

            if (_gastoModel == null)
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
                    await CargarCategorias();
                    return Page();
                }

                if (_gastoModel.id <= 0)
                {
                    message = "Id del gasto inválido.";
                    await CargarCategorias();
                    return Page();
                }

                var response = await _gastoService.UpdateGastoAsync(_gastoModel);
                if (response)
                {
                    return RedirectToPage("/Views/GastosView");
                }
                else
                {
                    message = "Error al actualizar el gasto.";
                    await CargarCategorias();
                    return Page();
                }
            }
            catch (Exception ex)
            {
                message = $"Error: {ex.Message}";
                await CargarCategorias();
                return Page();
            }
        }

        private async Task CargarCategorias()
        {
            var categorias = await _categoriaService.GetCategoriasAsync();
            CategoriasLista = categorias.Select(c => new SelectListItem
            {
                Value = c.id.ToString(),
                Text = c.nombre_categoria
            }).ToList();
        }
    }
}
