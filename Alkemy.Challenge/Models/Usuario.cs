using System.ComponentModel.DataAnnotations;

namespace Alkemy.Challenge.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string NickName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Correo { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }



    }
}
