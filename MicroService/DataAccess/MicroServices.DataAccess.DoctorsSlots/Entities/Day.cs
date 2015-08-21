using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MicroServices.DataAccess.DoctorsSlots.Interfaces;

namespace MicroServices.DataAccess.DoctorsSlots.Entities
{
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

        [Required]
        public DateTime Date { get; set; }

        [Key]
        public int Id { get; set; }
    }
}