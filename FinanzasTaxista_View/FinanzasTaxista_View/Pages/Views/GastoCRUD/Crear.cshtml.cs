using FinanzasTaxista_View.Models;
using FinanzasTaxista_View.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FinanzasTaxista_View.Pages.Views.GastoCRUD
{
    public class CrearModel : PageModel
    {
        private readonly GastoService _gastoService;
        private readonly CategoriaService _categoriaService;

        public CrearModel(GastoService gastoService, CategoriaService categoriaService)
        {
            _gastoService = gastoService;
            _categoriaService = categoriaService;
        }

        [BindProperty]
        public GastoModel _gastoModel { get; set; } = new();

        public List<SelectListItem> CategoriasLista { get; set; } = new();

        public string message { get; set; } = string.Empty;

        public async Task OnGetAsync()
        {
            // Llenar lista de categorías desde la API
            var categorias = await _categoriaService.GetCategoriasAsync();

            CategoriasLista = categorias.Select(c => new SelectListItem
            {
                Value = c.id.ToString(),
                Text = c.nombre_categoria
            }).ToList();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    message = "Datos inválidos.";
                    await OnGetAsync(); // recargar categorías
                    return Page();
                }

                var response = await _gastoService.AddGastoAsync(_gastoModel);
                message = response ? "Gasto agregado." : "Error al agregar un gasto.";
                return RedirectToPage("/Views/GastosView");
            }
            catch (Exception ex)
            {
                message = ex.Message;
                await OnGetAsync(); // recargar categorías
                return Page();
            }
        }
    }
}
