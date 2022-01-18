using Education.Domain;
using Education.Persistence;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Application.Cursos
{
    public  class CreateCursoCommand
    {
        public class CreateCursoCommandRequest : IRequest
        {
            public string Titulo { get; set; }
            public string Descripcion { get; set; }
            public DateTime FechaPublicacion { get; set; }
            public decimal Precio { get; set; }
        }
        public class CreateCursoCommandRequestValidation : AbstractValidator<CreateCursoCommandRequest>
        {
            public CreateCursoCommandRequestValidation()
            {
                RuleFor(x => x.Descripcion);
                RuleFor(x => x.Titulo);
            }
        }

        public class CreateCursoCommandHandler : IRequestHandler<CreateCursoCommandRequest>
        {
            private readonly EducationDbContext _context;
            public CreateCursoCommandHandler(EducationDbContext context)
            {
                _context = context;
            }
            public async Task<Unit> Handle(CreateCursoCommandRequest request, CancellationToken cancellationToken)
            {
                Curso curso = new()
                {
                    CursoId = Guid.NewGuid(),
                    Descripcion = request.Descripcion,
                    Titulo = request.Titulo,
                    FechaCreacion = DateTime.UtcNow,
                    FechaPublicacion = request.FechaPublicacion,
                    Precio = request.Precio
                };
                _context.Add(curso);
                var valor = await _context.SaveChangesAsync();
                if (valor > 0)
                    return Unit.Value;
                throw new Exception("No se ha podido insertar el curso");
            }
        }

    }
}
