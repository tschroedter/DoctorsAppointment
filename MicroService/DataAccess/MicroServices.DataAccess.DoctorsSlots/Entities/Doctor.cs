using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using MicroServices.DataAccess.DoctorsSlots.Interfaces;

namespace MicroServices.DataAccess.DoctorsSlots.Entities
{
    [ExcludeFromCodeCoverage]
    //ncrunch: no coverage start
    public class Doctor : IDoctor
    {
        public Doctor()
        {
            // ReSharper disable once DoNotCallOverridableMethodsInConstructor
            Days = new List <Day>();
        }

        public virtual ICollection <Day> Days { get; set; }

        public IEnumerable <IDay> AppointmentDays
        {
            get
            {
                return Days;
            }
        }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Key]
        public int Id { get; set; }
    }
}