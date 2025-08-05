using FinanzasTaxista_View.Models;
using FinanzasTaxista_View.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace FinanzasTaxista_View.Pages.Views.ViajeCRUD
{
    public class EditarModel : PageModel
    {
        private readonly ViajeService _viajeService;

        public EditarModel(ViajeService viajeService)
        {
            _viajeService = viajeService;
        }

        [BindProperty]
        public ViajeModel _viajeModel { get; set; } = new();

        public string message { get; set; } = string.Empty;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var viaje = await _viajeService.GetViajesAsync();

            _viajeModel = viaje.FirstOrDefault(u => u.id == id);

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
                    return Page();
                }

                // Verificar que el ID esté presente
                if (_viajeModel.id <= 0)
                {
                    message = "Id de viaje inválido.";
                    return Page();
                }

                var response = await _viajeService.UpdateViajeAsync(_viajeModel);
                if (response)
                {
                    message = "Viaje actualizado.";
                    return RedirectToPage("/Views/IngresosView");
                }
                else
                {
                    message = "Error al actualizar el viaje.";
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