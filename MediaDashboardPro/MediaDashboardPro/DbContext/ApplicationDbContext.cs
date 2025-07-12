using MediaDashboardPro.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace MediaDashboardPro.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Rol> Rol { get; set; }
        public DbSet<User> Usuario { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            // Rol - Usuario (Uno a muchos)
            modelBuilder.Entity<User>()
                .HasOne<Rol>() // Indica que la entidad 'User' tiene una relación con la entidad 'Rol'.
                .WithMany() // Declara que 'Rol' puede tener varios 'User'.  
                .HasForeignKey(u => u.rol_id) // Especifica que la clave foránea está en la entidad 'User'.
                .OnDelete(DeleteBehavior.Restrict);

        }
    }

}