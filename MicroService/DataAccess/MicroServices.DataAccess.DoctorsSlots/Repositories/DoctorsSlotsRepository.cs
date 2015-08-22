using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
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

            return FindSlotsForDoctor(doctors.First());
        }

        public IEnumerable <ISlot> FindSlotsForDoctorByDoctorId(int doctorId)
        {
            IDoctor doctor = m_Repository.FindById(doctorId);

            if ( doctor == null )
            {
                return new ISlot[0];
            }

            return FindSlotsForDoctor(doctor);
        }

        private List <ISlot> FindSlotsForDoctor(IDoctor doctor)
        {
            IEnumerable <IDay> days = doctor.AppointmentDays;
            var slots = new List <ISlot>();

            foreach ( IDay day in days )
            {
                slots.AddRange(day.AppointmentSlots());
            }

            return slots;
        }
    }
}