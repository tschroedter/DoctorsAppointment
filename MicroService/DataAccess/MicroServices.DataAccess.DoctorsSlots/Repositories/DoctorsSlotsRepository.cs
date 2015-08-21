using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using MicroServices.DataAccess.DoctorsSlots.Entities;
using MicroServices.DataAccess.DoctorsSlots.Interfaces;

namespace MicroServices.DataAccess.DoctorsSlots.Repositories
{
    public sealed class DoctorsSlotsRepository
        : IDoctorsSlotsRepository
    {
        private readonly IDoctorsRepository m_Repository;

        public DoctorsSlotsRepository([NotNull] IDoctorsRepository repository)
        {
            m_Repository = repository;
        }

        public IEnumerable <ISlot> FindSlotsForDoctorByLastName(string doctorLastName)
        {
            IDoctor[] doctors = m_Repository.FindByLastName(doctorLastName).ToArray();

            if ( !doctors.Any() ||
                 doctors.Count() > 1 )
            {
                return new ISlot[0];
            }

            IDoctor doctor = doctors.First();
            ICollection <Day> days = ( ( Doctor ) doctor ).Days;
            var slots = new List <Slot>();

            foreach ( Day day in days )
            {
                slots.AddRange(day.Slots);
            }

            return slots;
        }
    }
}