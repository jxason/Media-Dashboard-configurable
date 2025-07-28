using System.ComponentModel.DataAnnotations;

namespace FinanzasTaxista_Api.DTO_s
{
    public class UsuarioRegistroDTO
    {
        /*Aqui vamos a manejar directamente la informacion que utilizaremos
         para pasarsela a nuestro modelo usuario sin complicaciones y sin
        comprometer implementaciones de otros compañeros*/
        //Jason zuñiga/27/07/2025

        [Required]
        public string nombre_usuario { get; set; }

        [Required]
        public string apellido1 { get; set; }

        public string apellido2 { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string correo_electronico { get; set; }

        [Required]
        [StringLength(16, MinimumLength = 8)]
        [RegularExpression(@"^(?=(?:[^A-Za-z]*[A-Za-z]){3,})(?=(?:[^0-9]*[0-9]){3,})(?=[^!@#$%^&*(),.?\:{}|<>]*[!@#$%^&*(),.?\:{}|<>]).{8,16}$",
        ErrorMessage = "La contraseña debe tener entre 8 y 16 caracteres, al menos 3 letras, 3 números y 1 símbolo.")]

        public string contrasena { get; set; }

    

    }
}
