using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using proyectoprueba.datos.DatabaseConfiguration;
using proyectoprueba.entidades.Personas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace proyectoprueba.datos.mapping.persona
{
    public class Personamap : IEntityTypeConfiguration<Persona>
    {
        public void Configure( EntityTypeBuilder<Persona> builder)
        {
            builder.ToTable("Persona").HasKey(x=>x.PersonaId);
        }
    }
}
