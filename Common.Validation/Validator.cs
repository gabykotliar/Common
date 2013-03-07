using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Common.Validation
{
    public abstract class Validator
    {
        // TODO: el problema es que si tengo esto en un assembly separado de common no lo puedo poner en appservices.
        // y si lo meto en common todos van a tener que agregar referencias a DataAnnotations.

        public abstract ICollection<ValidationResult> Validate(object instance);

        public void ValidateAndThrow(object instance)
        {
            var error = Validate(instance).FirstOrDefault();

            if (error != null)
                throw new ValidationException(error.ErrorMessage);
        }

        public bool IsValid(object instance)
        {
            return !Validate(instance).Any();
        }
    }
}
