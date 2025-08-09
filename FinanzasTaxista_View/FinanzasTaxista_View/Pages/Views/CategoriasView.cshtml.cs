using FinanzasTaxista_View.Models;
using FinanzasTaxista_View.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FinanzasTaxista_View.Pages.Views
{
    [Authorize(Roles = "Administrador")]
    public class CategoriasViewModel : PageModel
    {
        private readonly CategoriaService _categoriasService;


        public CategoriasViewModel(CategoriaService categoriasService)
        {
            _categoriasService = categoriasService;
        }

        //lista de usuarios que se despliega en la VIEW
        public List<CategoriaModel> _Categorias { get; set; } = new List<CategoriaModel>();

        //Metodo que se ejecuta al cargar la pagina
        public async Task OnGetAsync()
        {

            _Categorias = await _categoriasService.GetCategoriasAsync();
        }

        public async Task<IActionResult> OnPostEliminarAsync(int id)
        {
            await _categoriasService.DeleteCategoriaAsync(id);
            return RedirectToPage(); // recarga la misma página con la lista actualizada
        }
    }
}
