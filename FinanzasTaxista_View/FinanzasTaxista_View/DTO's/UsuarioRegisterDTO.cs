using System.ComponentModel.DataAnnotations;

namespace FinanzasTaxista_View.Models.DTO_s
{
    public class UsuarioRegisterDTO
    {
        [Required]
        public string nombre_usuario { get; set; }

        [Required]
        public string apellido1 { get; set; }

        public string? apellido2 { get; set; }

        [Required]
        [EmailAddress]
        public string correo_electronico { get; set; }

        [Required]
        [StringLength(16, MinimumLength = 8, ErrorMessage = "Debe tener entre 8 y 16 caracteres.")]
        [RegularExpression(@"^(?=(?:.*[A-Za-z]){3,})(?=(?:.*\d){3,})(?=.*[!@#$%^&*()_+{}:;,.?~\\/-]).{8,}$",
            ErrorMessage = "Debe contener mínimo 3 letras, 3 números y 1 caracter especial.")]
        public string contrasena { get; set; }
    }
}
