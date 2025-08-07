
using FinanzasTaxista_Api.Models;
using Microsoft.EntityFrameworkCore;
namespace FinanzasTaxista_Api.DBContext
{
    public class ApplicationDBContext : DbContext
    {

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }


        // Definiendo las tablas de la base de datos.

        public DbSet<Rol> rol { get; set; }
        public DbSet<Usuario> usuario { get; set; }
        public DbSet<DiaTrabajo> dia_trabajo { get; set; }
        public DbSet<Categoria> categoria { get; set; }
        public DbSet<Viaje> viaje { get; set; }
        public DbSet<Gasto> gasto { get; set; }


    }
}
