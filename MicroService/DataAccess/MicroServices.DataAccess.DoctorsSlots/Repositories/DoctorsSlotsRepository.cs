﻿using System.Collections.Generic;
using JetBrains.Annotations;
using MicroServices.DataAccess.DoctorsSlots.Interfaces;
using Selkie.Windsor;

namespace MicroServices.DataAccess.DoctorsSlots.Repositories
{
    [ProjectComponent(Lifestyle.Transient)]
    public sealed class DoctorsSlotsRepository
        : IDoctorsSlotsRepository
    {
        private readonly IDoctorsRepository m_Repository;

        public DoctorsSlotsRepository([NotNull] IDoctorsRepository repository)
        {
            m_Repository = repository;
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