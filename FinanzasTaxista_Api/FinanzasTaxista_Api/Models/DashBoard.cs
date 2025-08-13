using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Finansastaxista_API.Models
{
    [Table("Dashboard")]
    public class Dashboard
    {
        [Key]
        public int Id { get; set; }  // Identity by default with EF Core and SQL Server

        public int UsuarioId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string JsonData { get; set; } = "{}";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
