using System.ComponentModel.DataAnnotations;

namespace FinanzasTaxista_Api.Models
{
    public class Categoria
    {

        [Key]
        public int id { get; set; }
        public string nombre_categoria { get; set; } = string.Empty;
    }
}
