using System.Data.Entity;
using System.Diagnostics.CodeAnalysis;
using MicroServices.DataAccess.DoctorsSlots.Entities;
using MicroServices.DataAccess.DoctorsSlots.Interfaces;

namespace MicroServices.DataAccess.DoctorsSlots.Contexts
{
    [ExcludeFromCodeCoverage]
    //ncrunch: no coverage start
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