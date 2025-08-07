using FinanzasTaxista_View.Models;
using FinanzasTaxista_View.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace FinanzasTaxista_View.Pages.Views.GastoCRUD
{
    public class EditarModel : PageModel
    {
        private readonly GastoService _gastoService;

        public EditarModel(GastoService gastoService)
        {
            _gastoService = gastoService;
        }

        [BindProperty]
        public GastoModel _gastoModel { get; set; } = new();

        public string message { get; set; } = string.Empty;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var gasto = await _gastoService.GetGastosAsync();

            _gastoModel = gasto.FirstOrDefault(g => g.id == id);

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
                    return Page();
                }

                // Verificar que el ID esté presente
                if (_gastoModel.id <= 0)
                {
                    message = "Id del gasto inválido.";
                    return Page();
                }

                var response = await _gastoService.UpdateGastoAsync(_gastoModel);
                if (response)
                {
                    message = "Gasto actualizado.";
                    return RedirectToPage("/Views/GastosView");
                }
                else
                {
                    message = "Error al actualizar el gasto.";
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
