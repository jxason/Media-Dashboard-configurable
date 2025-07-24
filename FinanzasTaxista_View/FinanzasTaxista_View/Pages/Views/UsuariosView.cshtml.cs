using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using FinanzasTaxista_View.Models;
using FinanzasTaxista_View.Service;

namespace FinanzasTaxista_View.Pages.Views
{
    public class UsuariosViewModel : PageModel
    {
     private readonly UsuariosService _usuariosService;
      

    public UsuariosViewModel(UsuariosService usuariosService)
        {
            _usuariosService = usuariosService;
        }

        //lista de usuarios que se despliega en la VIEW
        public List<UsuariosModels> _Usuarios { get; set; } = new List<UsuariosModels>();

        //Metodo que se ejecuta al cargar la pagina
        public async Task OnGetAsync()
        {

           _Usuarios = await _usuariosService.GetUsuariosAsync();
        }

    }
}
