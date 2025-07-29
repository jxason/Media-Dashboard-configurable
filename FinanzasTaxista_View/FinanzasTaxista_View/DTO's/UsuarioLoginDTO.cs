using System.ComponentModel.DataAnnotations;

namespace FinanzasTaxista_View.DTO_s
{
    public class UsuarioLoginDTO
    {
        [Required(ErrorMessage = "Este espacio es obligatorio")]
        public string identificador { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [MinLength(8, ErrorMessage = "Mínimo 8 caracteres")]
        public string contrasena { get; set; }
    }
}
