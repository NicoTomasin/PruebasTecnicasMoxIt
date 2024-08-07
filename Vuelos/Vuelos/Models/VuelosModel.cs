using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Vuelos.Models
{
    public class VuelosModel : IValidatableObject
    {
        public Guid Id { get; } = Guid.NewGuid();

        [Required(ErrorMessage = "El Numero de vuelo es requerido")]
        [StringLength(10)]
        public string FlightNumber { get; set; }

        [Required(ErrorMessage = "El horario de llegada es requerido")]
        public DateTime CheckIn { get; set; }

        [Required(ErrorMessage = "La aerolínea es requerida")]
        [StringLength(50)]
        public string Airline { get; set; }

        public bool IsDelayed { get; set; } = false;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (CheckIn <= DateTime.Now)
            {
                yield return new ValidationResult(
                    "El horario de llegada debe ser mayor a la fecha actual",
                    new[] { nameof(CheckIn) }
                );
            }
        }
    }
}
