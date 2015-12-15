using System;

namespace MicroServices.DataAccess.DoctorsSlots.Interfaces
{
    public interface ISlot : IEntity
    {
        DateTime StartDateTime { get; set; }
        DateTime EndDateTime { get; set; }
        SlotStatus Status { get; set; }
        int DayId { get; }
    }

    public enum SlotStatus
    {
        Unknown,
        Open,
        Booked
    }
}