
using FinanzasTaxista_Api.Models;
using Microsoft.EntityFrameworkCore;
namespace FinanzasTaxista_Api.DBContext
{
    public class AplicationDBContext : DbContext
    {

        public AplicationDBContext(DbContextOptions<AplicationDBContext> options) : base(options)
        {
        }


        //DEFINIENDO LAS TABLAS DE LA BD

        public DbSet<Usuarios> usuario { get; set; }


    }
}
