using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using MicroServices.DataAccess.DoctorsSlots.Interfaces;

namespace MicroServices.DataAccess.DoctorsSlots.Entities
{
    [ExcludeFromCodeCoverage]
    //ncrunch: no coverage start
    public class Day : IDay
    {
        public Day()
        {
            // ReSharper disable once DoNotCallOverridableMethodsInConstructor
            Slots = new List <Slot>();
        }

        public virtual ICollection <Slot> Slots { get; set; }

        [Required]
        public Doctor Doctor { get; set; }

        [ForeignKey("Doctor")]
        public int DoctorId { get; set; }

        public IEnumerable <ISlot> AppointmentSlots()
        {
            return Slots.ToArray();
        }

        [Required]
        public DateTime Date { get; set; }

        [Key]
        public int Id { get; set; }
    }
}