using AutoMapper;
using Education.Application.DTO;
using Education.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Application.Helper
{
    /* Mapper fake para las pruebas. */
    public class MappingTest : Profile
    {
        public MappingTest()
        {
            CreateMap<Curso, CursoDTO>().ReverseMap();
        }
    }
}
