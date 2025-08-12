using FinanzasTaxista_View.Models;
using FinanzasTaxista_View.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace FinanzasTaxista_View.Pages.Views.GastoCRUD
{
   
    public class CrearModel : PageModel
    {
        private readonly GastoService _gastoService;

        public CrearModel(GastoService gastoService)
        {
            _gastoService = gastoService;
        }

        [BindProperty]
        public GastoModel _gastoModel { get; set; } = new();
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

                var response = await _gastoService.AddGastoAsync(_gastoModel);
                message = response ? "Gasto agregado." : "Error al agregar un gasto.";
                return RedirectToPage("/Views/GastosView");
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return Page();
            }
        }
    }
}
