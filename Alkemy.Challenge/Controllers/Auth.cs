using Alkemy.Challenge.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Alkemy.Challenge.Controllers
{

    [ApiController]
    [Route("/auth")]

    public class Auth : ControllerBase
    {


        public Auth()
            :base()
        {

        }

        [HttpGet]
        public IEnumerable<Usuario> Get()
        {
            List<Usuario> result = new List<Usuario>();

            for(int i = 1; i < 10; i++)
            {
                Usuario usuario = new Usuario();
                usuario.Id = i;
                usuario.Correo = $"correo{i}@correo.com";
                usuario.NickName = $"Usuario{i}";
                usuario.Password = $"Password{i}";

                result.Add(usuario);
            }

            return result;
            
        }

        [HttpPost]
        [Route("/register")]
        public Usuario register(Usuario user)
        {

            return user;
        }

        [HttpPost]
        [Route("/login")]
        public Token login(string nickName, string password )
        {
            Token token = new Token();





            return token;
        }


    }
}
