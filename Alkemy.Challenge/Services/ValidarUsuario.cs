using Alkemy.Challenge.Data;
using Alkemy.Challenge.Models;
using System;
using System.Linq;

namespace Alkemy.Challenge.Services
{
    public static class ValidarUsuario
    {

        public static bool TokenIsValid(string tkn, ApplicationDbContext db)
        {
            try
            {

                DateTime dateTime = DateTime.Now;

                Token token = db.Tokens.Where(t => t.TokenUsuario.Equals(tkn)
                                              && (dateTime > t.FechaCreacion.Date
                                              && dateTime < t.FechaExpiracion.Date)).FirstOrDefault();

                if (token != null)
                    return true;

                return false;
            }
            catch (Exception)
            {

                return false;
            }
            

            

        }
    }
}
