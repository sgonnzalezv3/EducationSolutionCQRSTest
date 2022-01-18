using AutoFixture;
using AutoMapper;
using Education.Application.Helper;
using Education.Domain;
using Education.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Application.Cursos
{
    [TestFixture]
    public class CreateCursoCommandNUnitTest
    {
        private CreateCursoCommand.CreateCursoCommandHandler handlerCreateCurso;

        /* Lugar donde se inicializan los valores */
        [SetUp]
        public void Setup()
        {

            // ------------ Emular instancia de EF (context) ------------


            /* Instancia Fixture, que crea data de prueba */
            var fixture = new Fixture();

            /* Lista de data falsa */
            var cursoRecords = fixture.CreateMany<Curso>().ToList();

            /* Elemento con ID vacio */
            cursoRecords.Add(fixture.Build<Curso>()
                .With(x => x.CursoId, Guid.Empty)
                .Create());

            /* DbContext Fake trabajada con una bd de prueba en memoria */
            var options = new DbContextOptionsBuilder<EducationDbContext>().UseInMemoryDatabase(databaseName: $"EducationDbContext-{Guid.NewGuid()}")
                .Options;

            var educationDbContextFake = new EducationDbContext(options);
            educationDbContextFake.Cursos.AddRange(cursoRecords);
            educationDbContextFake.SaveChanges();

            // - - - - -  Emular el MappingProfile - - - - 
            var mapConfig = new MapperConfiguration(config =>
            {
                config.AddProfile(new MappingTest());
            });

            var mapper = mapConfig.CreateMapper();


            // Instanciar objeto de la clase GetCursoQuery.GetCursoQueryHandler y pasarle como 
            // paramaetros el context y el mapping

            handlerCreateCurso = new CreateCursoCommand.CreateCursoCommandHandler(educationDbContextFake);
        }

        [Test]

        public async Task CreateCursoCommand_InputCurso_ReturnInt()
        {
            CreateCursoCommand.CreateCursoCommandRequest request = new();

            request.FechaPublicacion = DateTime.UtcNow.AddDays(60);
            request.Titulo = "Pruebas";
            request.Descripcion = "Aprende a hacer pruebas unitarias";
            request.Precio = 39;

            var resultados = await handlerCreateCurso.Handle(request, new System.Threading.CancellationToken());

            Assert.That(resultados, Is.EqualTo(Unit.Value));
        }
    }
}
