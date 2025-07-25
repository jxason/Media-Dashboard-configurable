using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using FinanzasTaxista_View.Models;
using FinanzasTaxista_View.Service;

namespace FinanzasTaxista_View.Pages.Views
{
    public class UsuariosViewModel : PageModel
    {
     private readonly UsuarioService _usuariosService;
      

    public UsuariosViewModel(UsuarioService usuariosService)
        {
            _usuariosService = usuariosService;
        }

        //lista de usuarios que se despliega en la VIEW
        public List<UsuarioModel> _Usuarios { get; set; } = new List<UsuarioModel>();

        //Metodo que se ejecuta al cargar la pagina
        public async Task OnGetAsync()
        {

           _Usuarios = await _usuariosService.GetUsuariosAsync();
        }

    }
}
