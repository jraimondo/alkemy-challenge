using Alkemy.Challenge.Data;
using Alkemy.Challenge.Models;
using Alkemy.Challenge.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;

namespace Alkemy.Challenge.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Movies : ControllerBase
    {
        private readonly ApplicationDbContext db;

        public Movies(ApplicationDbContext db) 
        {
            this.db = db;
        }

        [HttpGet]
        [Route("detalle/{id}")]
        public ActionResult<PeliculaSerie> detalle(int id)
        {
            var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
            var token = authHeader.Parameter;

            if (!ValidarUsuario.TokenIsValid(token, db))
                return Unauthorized();

            PeliculaSerie peliculaSerie = db.PeliculasSeries.Where(p => p.Id == id).Include(p => p.Personajes).FirstOrDefault();

            return Ok(peliculaSerie);
        }


        [HttpGet]
        public ActionResult<IEnumerable<PeliculaSerie>> get([FromQuery] string name, [FromQuery] int genre, [FromQuery] string order)
        {
            var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
            var token = authHeader.Parameter;

            if (!ValidarUsuario.TokenIsValid(token, db))
                return Unauthorized();

            bool filter = false;
            List<PeliculaSerie> busquedaPeliculas = db.PeliculasSeries.ToList();


            if(name != null)
            {
                busquedaPeliculas = busquedaPeliculas.Where(p => p.Nombre.Contains(name)).ToList();
                filter = true;
            }else if (genre != 0)
            {
                busquedaPeliculas = db.Generos.Where(g => g.Id.Equals(genre)).Include(g => g.PeliculasSeries).SelectMany(
                    p => p.PeliculasSeries).ToList();
                filter = true;
            }
            if (order == "ASC")
            {
                busquedaPeliculas = busquedaPeliculas.OrderBy(p => p.FechaCreacion).ToList();
                filter = true;
            }
            else if (order == "DESC")
            {
                busquedaPeliculas = busquedaPeliculas.OrderByDescending(p => p.FechaCreacion).ToList();
                filter = true;
            }

            if(filter==false)
            {
                List<PeliculaSerieDTO> busquedaPeliculasDTO = busquedaPeliculas.Select(p => new PeliculaSerieDTO
                {
                    Nombre = p.Nombre,
                    Imagen = p.Imagen,
                    FechaCreacion = p.FechaCreacion
                }).ToList();

                return Ok(busquedaPeliculasDTO);
            }


            return Ok(busquedaPeliculas);
        }

        
        [HttpPost]
        public ActionResult<PeliculaSerie> post(PeliculaSerie peliculaSerie)
        {
            var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
            var token = authHeader.Parameter;

            if (!ValidarUsuario.TokenIsValid(token, db))
                return Unauthorized();

            if (ModelState.IsValid)
            {
                db.PeliculasSeries.Add(peliculaSerie);
                db.SaveChanges();

            }


            return Ok(peliculaSerie);

        }

        [HttpPut]
        public ActionResult<PeliculaSerie> put(PeliculaSerie peliculaSerie)
        {
            var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
            var token = authHeader.Parameter;

            if (!ValidarUsuario.TokenIsValid(token, db))
                return Unauthorized();

            PeliculaSerie nuevaPelicula = new PeliculaSerie()
            {
                Id = peliculaSerie.Id,
                Nombre = peliculaSerie.Nombre,
                Calificacion = peliculaSerie.Calificacion,
                Imagen = peliculaSerie.Imagen,
                Personajes = peliculaSerie.Personajes  


            };

            db.PeliculasSeries.Update(nuevaPelicula);
            db.SaveChanges();


            return Ok(nuevaPelicula);

        }

        [HttpDelete]
        public ActionResult delete(PeliculaSerie peliculaSerie)
        {
            var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
            var token = authHeader.Parameter;

            if (!ValidarUsuario.TokenIsValid(token, db))
                return Unauthorized();

            PeliculaSerie borrarPelicula = db.PeliculasSeries.FirstOrDefault(p => p.Id == peliculaSerie.Id);

            if (borrarPelicula != null)
            {
                db.Remove(borrarPelicula);
                db.SaveChanges();
                return Ok();
            }

            return BadRequest();

        }

    }
}
