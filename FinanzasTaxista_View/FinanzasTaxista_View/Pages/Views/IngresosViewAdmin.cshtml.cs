using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using FinanzasTaxista_View.Models;
using FinanzasTaxista_View.Service;

namespace FinanzasTaxista_View.Pages.Views
{
   
    public class IngresosViewAdminModel : PageModel
    {
        private readonly ViajeService _viajeService;


        public IngresosViewAdminModel(ViajeService viajeService)
        {
            _viajeService = viajeService;
        }

        
        public List<ViajeModel> _Viajes { get; set; } = new List<ViajeModel>();
        
        /*public async Task OnGetAsync()
        {

            _Viajes = await _viajeService.GetViajesAsync();
        }*/

        public async Task OnGetAsync()
        {
            var userIdClaim = User.FindFirst("UserId")?.Value;

            if (string.IsNullOrEmpty(userIdClaim))
            {
                _Viajes = new List<ViajeModel>();
                return;
            }

            var todosLosViajes = await _viajeService.GetViajesAsync();

            _Viajes = todosLosViajes
                .Where(v => v.id_usuario.ToString() == userIdClaim)
                .ToList();
        }

    }

}

