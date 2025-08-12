using FinanzasTaxista_View.Models;
using FinanzasTaxista_View.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FinanzasTaxista_View.Pages.Views.ViajeCRUD
{
    public class EditarModel : PageModel
    {
        private readonly ViajeService _viajeService;
        private readonly CategoriaService _categoriaService;

        public EditarModel(ViajeService viajeService, CategoriaService categoriaService)
        {
            _viajeService = viajeService;
            _categoriaService = categoriaService;
        }

        [BindProperty]
        public ViajeModel _viajeModel { get; set; } = new();

        public List<SelectListItem> CategoriasLista { get; set; } = new();

        public string message { get; set; } = string.Empty;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            // 1. Obtener lista de categorías
            var categorias = await _categoriaService.GetCategoriasAsync();
            CategoriasLista = categorias.Select(c => new SelectListItem
            {
                Value = c.id.ToString(),
                Text = c.nombre_categoria
            }).ToList();

            // 2. Obtener viaje por id
            var viaje = await _viajeService.GetViajesAsync();
            _viajeModel = viaje.FirstOrDefault(v => v.id == id);

            if (_viajeModel == null)
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
                    await CargarCategorias(); // recargar lista
                    return Page();
                }

                if (_viajeModel.id <= 0)
                {
                    message = "Id de viaje inválido.";
                    await CargarCategorias();
                    return Page();
                }

                var response = await _viajeService.UpdateViajeAsync(_viajeModel);
                if (response)
                {
                    return RedirectToPage("/Views/IngresosView");
                }
                else
                {
                    message = "Error al actualizar el viaje.";
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

        // Método para evitar repetir código al cargar categorías
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
