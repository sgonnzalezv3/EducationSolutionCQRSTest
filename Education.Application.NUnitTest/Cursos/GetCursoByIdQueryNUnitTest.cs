using AutoFixture;
using AutoMapper;
using Education.Application.Helper;
using Education.Domain;
using Education.Persistence;
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
    public class GetCursoByIdQueryNUnitTest
    {
        private GetCursoByIdQuery.GetCursoByIdQueryHandler handlerCursoById;
        private Guid cursoIdTest;


        /* Lugar donde se inicializan los valores */
        [SetUp]
        public void Setup()
        {

            // ------------ Emular instancia de EF (context) ------------
            cursoIdTest = new Guid("d66cc366-def4-44fa-90aa-29a2f5198b42");

            /* Instancia Fixture, que crea data de prueba */
            var fixture = new Fixture();

            /* Lista de data falsa */
            var cursoRecords = fixture.CreateMany<Curso>().ToList();

            /* Elemento con ID igual al guidId creado para probar */
            cursoRecords.Add(fixture.Build<Curso>()
                .With(x => x.CursoId, cursoIdTest)
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

            handlerCursoById = new GetCursoByIdQuery.GetCursoByIdQueryHandler(educationDbContextFake, mapper);
        }

        [Test]

        public async Task GetCursoByIdQueryHandler_InputCursoId_ReturnNotNull()
        {
            GetCursoByIdQuery.GetCursoByIdQueryRequest request = new()
            {
                Id = cursoIdTest
            };
            var resultado = await handlerCursoById.Handle(request, new System.Threading.CancellationToken());

            Assert.IsNotNull(resultado);
        }
    }
}
