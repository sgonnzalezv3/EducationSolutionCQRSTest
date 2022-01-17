using AutoMapper;
using Education.Application.DTO;
using Education.Domain;
using Education.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Application.Cursos
{
    public class GetCursoQuery
    {
        /* Representa los parametros que envia el cliente. */
        public class GetCursoQueryRequest : IRequest<List<CursoDTO>> { }

        /* Encargada de implementar la logica para realizar el query en la bd que retorna la lista de cursos. */
        public class GetCursoQueryHandler : IRequestHandler<GetCursoQueryRequest, List<CursoDTO>>
        {
            private readonly EducationDbContext _context;
            private readonly IMapper _mapper;
            public GetCursoQueryHandler(EducationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;

            }
            public async Task<List<CursoDTO>> Handle(GetCursoQueryRequest request, CancellationToken cancellationToken)
            {
                var cursos = await _context.Cursos.ToListAsync();
                var cursosDto = _mapper.Map<List<Curso>, List<CursoDTO>>(cursos);
                return cursosDto;
            }
        }
    }
}
