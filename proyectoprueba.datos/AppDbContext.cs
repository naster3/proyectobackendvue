using System;
using Microsoft.EntityFrameworkCore;
using proyectoprueba.entidades;

using proyectoprueba.datos.mapping.persona;
using proyectoprueba.entidades.Personas;

namespace proyectoprueba.datos
{
    public partial class AppDbContext : DbContext
    {
        public int _Persona;
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public virtual DbSet<Persona> Persona { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=Conexion");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new Personamap());
        }
    }
}
