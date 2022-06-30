using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alkemy.Challenge.Models
{
    public class Token
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public string TokenUsuario { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime FechaCreacion { get; set; }


        [Required]
        [DataType(DataType.DateTime)]
        public DateTime FechaExpiracion { get; set; }

        [Required]
        public Usuario Usuario { get; set; }


    }
}
