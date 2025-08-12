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
        private readonly CategoriaService _categoriaService;

        public IngresosViewAdminModel(ViajeService viajeService, CategoriaService categoriaService)
        {
            _viajeService = viajeService;
            _categoriaService = categoriaService;
        }

        public List<ViajeModel> _Viajes { get; set; } = new List<ViajeModel>();
        public List<CategoriaModel> Categorias { get; set; } = new List<CategoriaModel>();

        public async Task OnGetAsync()
        {
            var userIdClaim = User.FindFirst("UserId")?.Value;

            if (string.IsNullOrEmpty(userIdClaim))
            {
                _Viajes = new List<ViajeModel>();
                Categorias = new List<CategoriaModel>();
                return;
            }

            var todosLosViajes = await _viajeService.GetViajesAsync();
            _Viajes = todosLosViajes.Where(v => v.id_usuario.ToString() == userIdClaim).ToList();

            Categorias = await _categoriaService.GetCategoriasAsync();
        }

        // Método auxiliar para obtener el nombre de la categoría según id
        public string NombreCategoria(int id_categoria)
        {
            var categoria = Categorias.FirstOrDefault(c => c.id == id_categoria);
            return categoria != null ? categoria.nombre_categoria : "(Sin categoría)";
        }

    }

}

