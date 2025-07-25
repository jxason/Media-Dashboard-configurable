using System.ComponentModel.DataAnnotations;

namespace FinanzasTaxista_Api.Models
{
    public class Usuario
    {

        [Key]
        public int id { get; set; }
        public int id_rol { get; set; }
        
        public string nombre_usuario { get; set; } = string.Empty;

        public string apellido1 { get; set; } = string.Empty;

        public string apellido2 { get; set; } = string.Empty;

        public string correo_electronico { get; set; } = string.Empty;

        public string contrasena { get; set; } = string.Empty;
    
    }
}
