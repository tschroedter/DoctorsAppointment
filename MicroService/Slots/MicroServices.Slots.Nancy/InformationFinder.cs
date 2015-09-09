using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using MicroServices.DataAccess.DoctorsSlots.Entities;
using MicroServices.DataAccess.DoctorsSlots.Interfaces;
using MicroServices.Slots.Nancy.Interfaces;

namespace MicroServices.Slots.Nancy
{
    public class InformationFinder : IInformationFinder
    {
        private readonly ISlotsRepository m_Repository;

        public InformationFinder([NotNull] ISlotsRepository repository)
        {
            m_Repository = repository;
        }

        public ISlotForResponse FindById(int id)
        {
            ISlot slot = m_Repository.FindById(id);

            if ( slot == null )
            {
                return null;
            }

            return new SlotForResponse(slot);
        }

        public IEnumerable <ISlotForResponse> List()
        {
            IEnumerable <ISlot> all = m_Repository.All;

            SlotForResponse[] slots = all.Select(x => new SlotForResponse(x))
                                         .ToArray();

            return slots;
        }

        public ISlotForResponse Save(ISlotForResponse slot)
        {
            ISlot toBeUpdated = ToSlot(slot);

            m_Repository.Save(toBeUpdated);

            return new SlotForResponse(toBeUpdated);
        }

        public ISlotForResponse Delete(int id)
        {
            ISlot slot = m_Repository.FindById(id);

            if ( slot == null )
            {
                return null;
            }

            m_Repository.Remove(slot);

            return new SlotForResponse(slot);
        }

        private ISlot ToSlot(ISlotForResponse slot)
        {
            return new Slot
                   {
                       Id = slot.Id,
                       DayId = slot.DayId,
                       StartDateTime = slot.StartDateTime,
                       EndDateTime = slot.EndDateTime,
                       Status = slot.Status
                   };
        }
    }
}