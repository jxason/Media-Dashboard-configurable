namespace FinanzasTaxista_View.Models
{
    public class UsuarioModel
    {
        public int id { get; set; }

        public string nombre_usuario { get; set; } = string.Empty;

        public string apellido1 { get; set; } = string.Empty;

        public string apellido2 { get; set; } = string.Empty;

        public string correo_electronico { get; set; } = string.Empty;

        public string contrasena { get; set; } = string.Empty;

        public int id_rol { get; set; }


    }
}
