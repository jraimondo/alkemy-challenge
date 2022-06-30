using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Alkemy.Challenge.Models
{
    public class Personaje:IPersonaje
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public int Edad { get; set; }
        
        public int Peso { get; set; }

        public string Imagen { get; set; }

        public string Historia { get; set; }


        public ICollection<PeliculaSerie> PeliculasSeries { get; set; }
    }
}
