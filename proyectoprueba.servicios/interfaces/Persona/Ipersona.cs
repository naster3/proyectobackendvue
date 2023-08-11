using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using proyectoprueba.servicios.viewmodels.Personaviewmodel;

namespace proyectoprueba.servicios.interfaces.Persona
{
    public interface IPersona
    {
        Task<List<PersonaViewModels>> MostrarDatos();
        Task<PersonaViewModels> BuscarPersona(int id);
        Task<int> CrearPersona(int id, PersonaViewModels persona);
        Task<int> ModificarPersona(int id, PersonaViewModels persona);
       // Task<bool> ExistePersona(int id);
        Task<int> EliminarPersona(int id);
    }
}
