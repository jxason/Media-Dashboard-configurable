using System.ComponentModel.DataAnnotations;

namespace FinanzasTaxista_Api.Models
{
    public class DiaTrabajo
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int IdUsuario { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; }
      
    }
}
