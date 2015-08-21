using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MicroServices.DataAccess.DoctorsSlots.Interfaces;

namespace MicroServices.DataAccess.DoctorsSlots.Entities
{
    public class Slot : ISlot
    {
        [Required]
        public Day Day { get; set; }

        [Required]
        public SlotStatus Status { get; set; }

        [Required]
        public DateTime StartDateTime { get; set; }

        [Required]
        public DateTime EndDateTime { get; set; }

        [ForeignKey("Day")]
        public int DayId { get; set; }

        [Key]
        public int Id { get; set; }
    }
}