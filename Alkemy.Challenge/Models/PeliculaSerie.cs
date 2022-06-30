using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Alkemy.Challenge.Models
{
    public class PeliculaSerie
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }

        public DateTime FechaCreacion { get; set; }

        public int Calificacion { get; set; }

        public string Imagen { get; set; }

        public ICollection<Personaje> Personajes { get; set; }

    }
}
