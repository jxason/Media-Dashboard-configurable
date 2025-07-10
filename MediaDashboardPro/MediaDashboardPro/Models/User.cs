using System.ComponentModel.DataAnnotations;

namespace MediaDashboardPro.Models
{
    public class User
    {

        [Key]
        public int id { get; set; }
        public string nombre { get; set; } = string.Empty;
        public string apellidos { get; set; } = string.Empty;

        [Required(ErrorMessage = "El username del usuario es obligatorio.")]
        [StringLength(50, ErrorMessage = "El username del usuario no puede tener más de 100 caracteres.")]
        public string nombre_usuario { get; set; } = string.Empty;

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [StringLength(255, ErrorMessage = "La contraseña no puede tener más de 255 caracteres.")]
        public string password { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public DateTime fecha_registro { get; set; }
        public int rol_id { get; set; }

    }
}
