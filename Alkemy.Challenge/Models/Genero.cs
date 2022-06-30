using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Alkemy.Challenge.Models
{
    public class Genero
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Imagen { get; set; }

        public ICollection<PeliculaSerie> PeliculasSeries { get; set; }
    }
}
