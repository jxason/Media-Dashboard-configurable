using System.ComponentModel.DataAnnotations;

namespace FinanzasTaxista_Api.Models
{
    public class Viaje
    {
        [Key]
        public int id { get; set; }

        public int id_usuario { get; set; }

        public int id_dia { get; set; }

        public int id_categoria { get; set; }

        public decimal monto { get; set; }

        public string ubicacion { get; set; } = string.Empty;
    
    }
}
