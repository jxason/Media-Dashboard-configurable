using System.ComponentModel.DataAnnotations;

namespace FinanzasTaxista_Api.Models
{
    public class DiaTrabajo
    {
        [Key]
        public int id { get; set; }
        public DateOnly fecha { get; set; }
    
    }
}
