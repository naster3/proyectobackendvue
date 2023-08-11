using proyectoprueba.entidades.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace proyectoprueba.entidades.Personas
{
    public partial class Persona
    {
       
        [Required(ErrorMessage = MessageServices.InvalidPersona)]
        public int PersonaId{ get; set; }
        [Required(ErrorMessage = MessageServices.InvalidPersona)]
        public string PerNombre { get; set; }
        [Required(ErrorMessage = MessageServices.InvalidPersona)]
        public string PerApellido { get; set; }

    }
}
