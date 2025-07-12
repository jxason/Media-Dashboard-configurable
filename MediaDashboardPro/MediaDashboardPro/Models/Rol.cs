using System.ComponentModel.DataAnnotations;

namespace MediaDashboardPro.Models
{
    public class Rol
    {

        [Key]
        public int id { get; set; }
        
        public string nombre_rol { get; set; } = string.Empty;
    }
}
