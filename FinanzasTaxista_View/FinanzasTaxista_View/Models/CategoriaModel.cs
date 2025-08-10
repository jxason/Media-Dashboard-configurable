using System.ComponentModel.DataAnnotations;

namespace FinanzasTaxista_View.Models
{
    public class CategoriaModel
    {
        public int id { get; set; }

        [Required(ErrorMessage = "El nombre de la categoria es obligatorio.")]
        public string nombre_categoria { get; set; } = string.Empty;
    
    }
}
