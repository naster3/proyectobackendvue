using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore;
using proyectoprueba.datos;
using proyectoprueba.servicios.repositorios.PersonaRepo;
using proyectoprueba.servicios.viewmodels.Personaviewmodel;
using proyectoprueba.web.Services.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace proyectoprueba.web.Controllers.Persona
{
    [Route("api/Persona")]
    [ApiController]
    public class PersonaController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly PersonaRepo _personaRepo;
        public PersonaController(AppDbContext context, PersonaRepo personaRepo) {
            _appDbContext = context;
            _personaRepo = personaRepo;
        }
        [HttpGet]
        public async Task<List<PersonaViewModels>> Getmostrardatos()
        {
            try
            {
             List<PersonaViewModels> persona = await _personaRepo.MostrarDatos();
                return persona;
            }
            catch(Exception ex)
            {
                throw new Exception($"{nameof(PersonaController)}genero la siguiente excepcion: {ex.Message}");
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<PersonaViewModels>> Getbuscarpersona(int id)
        {
            try
            {
               var spersona= await _personaRepo.BuscarPersona(id);
                return Ok(spersona);
            }
            catch(Exception ex)
            {
                throw new Exception($"{nameof(PersonaController)}genero la siguiente excepcion: {ex.Message}");
            }
        }
        [HttpPost]
        public async Task<ActionResult<PersonaViewModels>> Postcrearpersona(int id, [FromBody] PersonaViewModels persona)
        {
            try
            {  
                id = await _personaRepo.CrearPersona(id, persona);
                return Ok(id);
            }
            catch(Exception ex)
            {
                throw new Exception($"{nameof(PersonaController)}genero la siguiente excepcion: {ex.Message}");
            }
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<PersonaViewModels>> Putmoficarpersona( int id, [FromBody]PersonaViewModels persona)
        {
            try
            {
                int admitidos = await _personaRepo.ModificarPersona(id,persona);
                return Ok(admitidos);
            }
            catch( Exception ex)
            {
                throw new Exception($"{nameof(PersonaController)}genero la siguiente excepcion: {ex.Message}");
            }
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<PersonaViewModels>> Eliminarpersonas(int id)
        {
            try
            {
               
                int eliminado = await _personaRepo.EliminarPersona(id);
                return Ok(eliminado);
            }
            catch(Exception ex )
            {
                throw new Exception($"{nameof(PersonaController)}genero la siguiente excepcion: {ex.Message}");
            }
        }
    }
}
