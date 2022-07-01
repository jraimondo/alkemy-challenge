using Alkemy.Challenge.Data;
using Alkemy.Challenge.DTO_s;
using Microsoft.EntityFrameworkCore;
using Alkemy.Challenge.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Net.Http.Headers;
using System.Text;
using Alkemy.Challenge.Services;

namespace Alkemy.Challenge.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class Characters : ControllerBase
    {
        private readonly ApplicationDbContext db;

        public Characters(ApplicationDbContext db)
            
        {
            this.db = db;
        }

        [HttpGet]
        [Route("detalle/{id}")]
        public ActionResult<Personaje> detalle(int id)
        {

            Personaje personaje = db.Personajes.Where(p => p.Id == id).Include(p => p.PeliculasSeries).FirstOrDefault();


            return Ok(personaje);
        }


        [HttpGet]
        [Route("/characters")]
        public ActionResult<IEnumerable<IPersonaje>> get([FromQuery] string name, [FromQuery] int age, [FromQuery] int movies)
        {

            var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
            var token = authHeader.Parameter;

            if (!ValidarUsuario.TokenIsValid(token,db))
                   return Unauthorized();
           

            bool filter = false;

            List<Personaje> busquedaPersonajes = db.Personajes.Include(p => p.PeliculasSeries).ToList();

            if(name != null)
            {
                busquedaPersonajes = busquedaPersonajes.Where(p => p.Nombre.Contains(name)).ToList();
                filter = true;
            }
            if(age != 0)
            {
                busquedaPersonajes = busquedaPersonajes.Where(p => p.Edad.Equals(age)).ToList();
                filter = true;
            }
            if(movies !=0)
            {
                var movie = db.PeliculasSeries.Find(movies);
                busquedaPersonajes = busquedaPersonajes.Where(p => p.PeliculasSeries.Equals(movie)).ToList();
                filter = true;
            }
            if (filter.Equals(false))
            {
                List<PersonajeDTO> busquedaPersonajeDTO = busquedaPersonajes.Select(p => new PersonajeDTO()
                {
                    Nombre = p.Nombre,
                    Imagen = p.Imagen

                }).ToList();

                return Ok(busquedaPersonajeDTO);

            }


            return Ok(busquedaPersonajes);
        }


        [HttpPost]
        public ActionResult<Personaje> post(Personaje personaje)
        {
            var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
            var token = authHeader.Parameter;

            if (!ValidarUsuario.TokenIsValid(token, db))
                return Unauthorized();

            if (ModelState.IsValid)
            {
                db.Personajes.Add(personaje);
                db.SaveChanges();

            }


            return Ok(personaje);

        }

        [HttpPut]
        public ActionResult<Personaje> put(Personaje personaje)
        {
            var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
            var token = authHeader.Parameter;

            if (!ValidarUsuario.TokenIsValid(token, db))
                return Unauthorized();

            Personaje nuevoPersonaje = new Personaje()
            {
                Id = personaje.Id,
                Nombre = personaje.Nombre,
                Edad = personaje.Edad,
                Peso = personaje.Peso,
                Historia = personaje.Historia,
                Imagen = personaje.Imagen,

            };

            db.Personajes.Update(nuevoPersonaje);
            db.SaveChanges();


            return Ok(personaje);

        }


        [HttpDelete]
        public ActionResult delete(Personaje personaje)
        {
            var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
            var token = authHeader.Parameter;

            if (!ValidarUsuario.TokenIsValid(token, db))
                return Unauthorized();

            Personaje BorrarPersonaje = db.Personajes.FirstOrDefault(p => p.Id == personaje.Id);

            if (BorrarPersonaje != null)
            {
                db.Remove(BorrarPersonaje);
                db.SaveChanges();
                return Ok();
            }

            return BadRequest();


        }







    }
}
