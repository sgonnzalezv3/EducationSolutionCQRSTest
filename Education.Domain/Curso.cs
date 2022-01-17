using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Domain
{
    public class Curso
    {
        [Key]
        public Guid CursoId { get; set; }
        [Required]
        [StringLength(100)]
        public string Titulo { get; set; }
        
        [Required]
        [StringLength(200)]
        public string Descripcion { get; set; }
        [DataType(DataType.Date)]
        //MAYOR que fecha actual
        [DateInFuture]
        public DateTime? FechaPublicacion { get; set; }
        [DataType(DataType.Date)]
        public DateTime? FechaCreacion { get; set; }
        public Decimal Precio { get; set; }
    }
}
