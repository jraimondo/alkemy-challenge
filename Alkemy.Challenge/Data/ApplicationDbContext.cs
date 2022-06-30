using Alkemy.Challenge.Models;
using Microsoft.EntityFrameworkCore;

namespace Alkemy.Challenge.Data
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Genero> Generos { get; set; }
        public DbSet<Personaje> Personajes { get; set; }
        public DbSet<PeliculaSerie> PeliculasSeries { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Token> Tokens { get; set; }

    }
}
