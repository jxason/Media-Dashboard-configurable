using FinanzasTaxista_View.Models;
using FinanzasTaxista_View.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace FinanzasTaxista_View.Pages.Views.ViajeCRUD
{

    public class CrearModel : PageModel
    {
        private readonly ViajeService _viajeService;

        public CrearModel(ViajeService viajeService)
        {
            _viajeService = viajeService;
        }

        [BindProperty]
        public ViajeModel _viajeModel { get; set; } = new();
        public string message { get; set; } = string.Empty;

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    message = "Datos inválidos.";
                    return Page();
                }

                var response = await _viajeService.AddViajeAsync(_viajeModel);
                message = response ? "Viaje agregado." : "Error al crear un viaje.";
                return RedirectToPage("/Views/IngresosView");
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return Page();
            }
        }
    }
}
