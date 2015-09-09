using System;
using MicroServices.DataAccess.DoctorsSlots.Interfaces;
using MicroServices.Slots.Nancy.Interfaces;

namespace MicroServices.Slots.Nancy
{
    public class SlotForResponse : ISlotForResponse
    {
        public SlotForResponse()
        {
        }

        public SlotForResponse(ISlot slot)
        {
            Status = slot.Status;
            StartDateTime = slot.StartDateTime;
            EndDateTime = slot.EndDateTime;
            DayId = slot.DayId;
            Id = slot.Id;
        }

        public int Id { get; set; }
        public int DayId { get; set; }
        public DateTime EndDateTime { get; set; }
        public DateTime StartDateTime { get; set; }
        public SlotStatus Status { get; set; }
    }
}