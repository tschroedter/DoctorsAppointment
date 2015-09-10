using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using MicroServices.DataAccess.DoctorsSlots.Interfaces;
using MicroServices.DoctorsSlots.Nancy.Interfaces;
using Selkie.Windsor;

namespace MicroServices.DoctorsSlots.Nancy
{
    [ProjectComponent(Lifestyle.Transient)]
    public class InformationFinder : IInformationFinder
    {
        private readonly IDoctorsSlotsRepository m_Repository;

        public InformationFinder([NotNull] IDoctorsSlotsRepository repository)
        {
            m_Repository = repository;
        }

        public IEnumerable <ISlot> List(int doctorId,
                                        string date,
                                        string status)
        {
            IEnumerable <ISlot> slots = m_Repository.FindSlotsForDoctorByDoctorId(doctorId);

            slots = FilterSlotsByDate(slots,
                                      date);

            slots = FilterSlotsByStatus(slots,
                                        status);

            return slots;
        }

        private IEnumerable <ISlot> FilterSlotsByDate(IEnumerable <ISlot> slots,
                                                      string date)
        {
            DateTime dateTime;

            if ( DateTime.TryParse(date,
                                   out dateTime) )
            {
                slots = slots.Where(x => x.StartDateTime.Date == dateTime.Date);
            }

            return slots;
        }

        private IEnumerable <ISlot> FilterSlotsByStatus(IEnumerable <ISlot> slots,
                                                        string status)
        {
            SlotStatus slotStatus;

            if ( Enum.TryParse(status,
                               true,
                               out slotStatus) )
            {
                slots = slots.Where(x => x.Status == slotStatus);
            }

            return slots;
        }
    }
}