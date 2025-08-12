using Microsoft.AspNetCore.Mvc.RazorPages;
using FinanzasTaxista_View.Models;
using FinanzasTaxista_View.Service;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanzasTaxista_View.Pages.Views
{
    public class GastosViewAdminModel : PageModel
    {
        private readonly GastoService _gastoService;
        private readonly CategoriaService _categoriaService;

        public GastosViewAdminModel(GastoService gastoService, CategoriaService categoriaService)
        {
            _gastoService = gastoService;
            _categoriaService = categoriaService;
        }

        public List<GastoModel> _Gastos { get; set; } = new List<GastoModel>();
        public List<CategoriaModel> Categorias { get; set; } = new List<CategoriaModel>();

        public async Task OnGetAsync()
        {
            var userIdClaim = User.FindFirst("UserId")?.Value;

            if (string.IsNullOrEmpty(userIdClaim))
            {
                _Gastos = new List<GastoModel>();
                Categorias = new List<CategoriaModel>();
                return;
            }

            var todosLosGastos = await _gastoService.GetGastosAsync();
            _Gastos = todosLosGastos
                .Where(v => v.id_usuario.ToString() == userIdClaim)
                .ToList();

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
