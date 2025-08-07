using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using FinanzasTaxista_View.Models;
using FinanzasTaxista_View.Service;

namespace FinanzasTaxista_View.Pages.Views
{
    public class GastosViewModel : PageModel
    {
        private readonly GastoService _gastoService;


        public GastosViewModel(GastoService gastoService)
        {
            _gastoService = gastoService;
        }


        public List<GastoModel> _Gastos { get; set; } = new List<GastoModel>();

        public async Task OnGetAsync()
        {

            _Gastos = await _gastoService.GetGastosAsync();
        }

    }

}
