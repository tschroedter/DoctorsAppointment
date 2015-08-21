using System.Data.Entity;
using MicroServices.DataAccess.DoctorsSlots.Entities;
using MicroServices.DataAccess.DoctorsSlots.Interfaces;

namespace MicroServices.DataAccess.DoctorsSlots.Contexts
{
    public class DoctorsSlotsContext
        : DbContext,
          IDoctorsSlotsContext
    {
        public DoctorsSlotsContext()
            : base("Doctors.Application")
        {
        }

        public DbSet <Doctor> Doctors { get; set; }
        public DbSet <Day> Days { get; set; }
        public DbSet <Slot> Slots { get; set; }
    }
}