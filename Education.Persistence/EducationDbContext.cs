using Education.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Persistence
{
    public class EducationDbContext : DbContext
    {
        public EducationDbContext(DbContextOptions<EducationDbContext> options) : base(options)
        {

        }

        public DbSet<Curso> Cursos { get; set; }

        /* Inicializar valores prueba */
        /* Esto ocurre cuando se ejecuta el proceso de migracion */
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            /* Parametrizando la cantidad de decimales que va a tener la columna precio (14 numeros, dos de ellos decimales) */
            modelBuilder.Entity<Curso>().Property(x => x.Precio).HasPrecision(14, 2);

            modelBuilder.Entity<Curso>().HasData(
                    new Curso
                    {
                        CursoId = Guid.NewGuid(),
                        Descripcion = "Curso de Test",
                        FechaCreacion = DateTime.Now,
                        FechaPublicacion = DateTime.Now.AddYears(2),
                        Precio = 56,
                        Titulo = "Curso de pruebas"
                    }
                );

            modelBuilder.Entity<Curso>().HasData(
                    new Curso
                    {
                        CursoId = Guid.NewGuid(),
                        Descripcion = "Docker",
                        FechaCreacion = DateTime.Now,
                        FechaPublicacion = DateTime.Now.AddYears(50),
                        Precio = 560,
                        Titulo = "Curso de Docker"
                    }
                );
        }
    }
}
