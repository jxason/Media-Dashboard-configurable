using FinanzasTaxista_View.Models;
using FinanzasTaxista_View.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FinanzasTaxista_View.Pages.Views.ViajeCRUD
{
    public class CrearModel : PageModel
    {
        private readonly ViajeService _viajeService;
        private readonly CategoriaService _categoriaService; // Servicio para categorías

        public CrearModel(ViajeService viajeService, CategoriaService categoriaService)
        {
            _viajeService = viajeService;
            _categoriaService = categoriaService;
        }

        [BindProperty]
        public ViajeModel _viajeModel { get; set; } = new();

        public List<SelectListItem> CategoriasLista { get; set; } = new();

        public string message { get; set; } = string.Empty;

        public async Task OnGetAsync()
        {
            // Llamada a tu API para obtener las categorías
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
                    await OnGetAsync(); // Recargar categorías si falla
                    return Page();
                }

                var response = await _viajeService.AddViajeAsync(_viajeModel);
                message = response ? "Viaje agregado." : "Error al crear un viaje.";
                return RedirectToPage("/Views/IngresosView");
            }
            catch (Exception ex)
            {
                message = ex.Message;
                await OnGetAsync();
                return Page();
            }
        }
    }
}
