using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using proyectoprueba.datos;
using proyectoprueba.entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using proyectoprueba.servicios.interfaces;
using proyectoprueba.servicios.viewmodels;
using proyectoprueba.servicios.interfaces.Persona;
using proyectoprueba.servicios.viewmodels.Personaviewmodel;
using proyectoprueba.entidades.Personas;

namespace proyectoprueba.servicios.repositorios.PersonaRepo
{
    public class PersonaRepo : IPersona
    {
        private readonly AppDbContext _context;
      

        public PersonaRepo(AppDbContext context) {
            _context = context;

            
        }
        public async Task<List< PersonaViewModels>> MostrarDatos()
        {
            try
            {
                List<PersonaViewModels> persona = await _context.Persona.Select(x => new PersonaViewModels
                {
                    Id_persona = x.PersonaId,
                    Apellido = x.PerApellido,
                    Nombre = x.PerNombre

                }).ToListAsync();
                return persona;
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(PersonaRepo)}genero la siguiente excepcion: {ex.Message}");
            }
           
           
        }
        public async Task<PersonaViewModels> BuscarPersona(int id)
        {
            try
            {
                
                PersonaViewModels persona = await _context.Persona.AsNoTracking().Where(x => x.PersonaId == id).Select(x => new PersonaViewModels
                {
                    Id_persona = x.PersonaId,
                    Nombre = x.PerNombre,
                    Apellido = x.PerApellido
                }).FirstOrDefaultAsync();
                return persona;
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(PersonaRepo)}genero la siguiente excepcion: {ex.Message}");
            }
           
        }
        public async Task<int> CrearPersona (int id, PersonaViewModels entidad)
        {
            bool existe = await _context.Persona.AnyAsync(t => t.PerNombre == entidad.Nombre && t.PerApellido == entidad.Apellido);
            if (existe)
            {
                throw new Exception($"{nameof(PersonaRepo)}No se pueden agregar los valores ya existentes.");
            }
            else
            {
                if (entidad.Id_persona == 0)
                {
                    var personaAdd = new Persona
                    {
                        PersonaId = entidad.Id_persona,
                        PerNombre = entidad.Nombre,
                        PerApellido = entidad.Apellido
                    };
                    await _context.AddAsync(personaAdd);
                    await _context.SaveChangesAsync();
                    id = personaAdd.PersonaId;
                }

            }
            
            return id;
        }
        public async Task<int> ModificarPersona(int id, PersonaViewModels entidad)
        {
            bool existe = await _context.Persona.AnyAsync(t => t.PerNombre == entidad.Nombre && t.PerApellido == entidad.Apellido);

            if (!existe)
                if (!string.IsNullOrEmpty(entidad.Nombre) || string.IsNullOrEmpty(entidad.Apellido))
                {
                    if (id != entidad.Id_persona)
                    {
                        var modificarpersona = await _context.Persona.Where(x => x.PersonaId == id).FirstOrDefaultAsync();
                        modificarpersona.PerNombre = entidad.Nombre;
                        modificarpersona.PerApellido = entidad.Apellido;
                        await _context.SaveChangesAsync();
                        return modificarpersona.PersonaId;
                    }

                }
                else
                {
                    throw new Exception($"{nameof(PersonaRepo)}No puedes editar valores nulos.");
                }
            //bool existe = await _context.Persona.AnyAsync(t => t.PerNombre == entidad.Nombre && t.PerApellido == entidad.Apellido);
            //if (existe)
            //    if (string.IsNullOrEmpty(entidad.Nombre)|| string.IsNullOrEmpty(entidad.Apellido))
            //{

            //    throw new Exception($"{nameof(PersonaRepo)}No puedes editar valores nulos.");
            //}
            //else
            //{
            //    if (id != entidad.Id_persona)
            //    {
            //        var modificarpersona = await _context.Persona.Where(x => x.PersonaId == id).FirstOrDefaultAsync();
            //        modificarpersona.PerNombre = entidad.Nombre;
            //        modificarpersona.PerApellido = entidad.Apellido;
            //        await _context.SaveChangesAsync();
            //        return modificarpersona.PersonaId;
            //    }
            //}
            return id;
        }
       public async Task<int> EliminarPersona(int id)
        {
            // int id = 0;
            var personadet = await _context.Persona.Where(x=> x.PersonaId== id).FirstOrDefaultAsync();
            _context.Persona.Remove(personadet);
            await _context.SaveChangesAsync();
            return id;
        }
    }
}
