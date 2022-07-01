using Alkemy.Challenge.Data;
using Alkemy.Challenge.DTO_s;
using Alkemy.Challenge.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Alkemy.Challenge.Controllers
{

    [ApiController]
    [Route("/auth")]

    public class Auth : ControllerBase
    {
        private readonly ApplicationDbContext db;

        public Auth(ApplicationDbContext db) 
        {
            this.db = db;
        }

        [HttpGet]
        public ActionResult<IEnumerable<UsuarioDTO>> Get()
        {
            List<UsuarioDTO> result = new List<UsuarioDTO>();

            try
            {
                result = db.Usuarios.Select(u => new UsuarioDTO()
                {
                    Id = u.Id,
                    NickName = u.NickName,
                    Correo = u.Correo,
                }).ToList();
            }
            catch (System.Exception ex)
            {

                return BadRequest(ex);
            }
            

            return result;
            
        }

        [HttpPost]
        [Route("register")]
        public ActionResult<Usuario> register(Usuario user)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    db.Usuarios.Add(user);
                    db.SaveChanges();

                }


            }
            catch (System.Exception ex)
            {

                return BadRequest(ex.Message);
            }


            return Ok(user);
        }


        [HttpPost]
        [Route("login")]
        public ActionResult<Token> login(LoginDTO login )
        {
            Token token = new();

            try
            {

                var user = db.Usuarios.Where(u => u.NickName.Equals(login.NickName) && u.Password.Equals(login.Password)).FirstOrDefault();

                if (user == null)
                {
                    return BadRequest("Error en el usuario o password");
                }
                else
                {

                    token.Usuario = user;
                    token.TokenUsuario = Guid.NewGuid().ToString();
                    token.FechaCreacion = DateTime.UtcNow;
                    token.FechaExpiracion = DateTime.UtcNow.AddDays(1);

                    db.Tokens.Add(token);
                    db.SaveChanges();

                    return Ok(token);
                }

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
            





            
        }


    }
}
