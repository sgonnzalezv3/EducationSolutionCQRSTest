using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Domain
{
    /* Clase que valida que la fecha sea superior a la actual */
    public class DateInFutureAttribute : ValidationAttribute
    {
        private readonly Func<DateTime> _datetimeNowProvider;

        public DateInFutureAttribute() : this(() => DateTime.Now) {}
        public DateInFutureAttribute(Func<DateTime> datetimeNowProvider)
        {
            _datetimeNowProvider = datetimeNowProvider;
            ErrorMessage = "La fecha debe ser superior a la actual";
        }

        /* Validar si la fecha es valida */
        public override bool IsValid(object value)
        {
            bool isValid = false;
            /* casteo entre el valor y date time */
            if(value is DateTime dateTime)
            {

                isValid = dateTime > _datetimeNowProvider();
            }
            return isValid;
        }
    }
}
