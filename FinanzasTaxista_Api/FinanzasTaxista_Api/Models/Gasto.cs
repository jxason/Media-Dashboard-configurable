using System.ComponentModel.DataAnnotations;

namespace FinanzasTaxista_Api.Models
{
    public class Gasto
    {
        [Key]
        public int id { get; set; }

        public int id_usuario { get; set; }

        public int id_dia { get; set; }

        public int id_categoria { get; set; }

        public decimal monto { get; set; }
    
    }
}
