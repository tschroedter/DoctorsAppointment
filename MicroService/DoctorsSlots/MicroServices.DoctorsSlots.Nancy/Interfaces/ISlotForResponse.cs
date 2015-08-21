using System;
using MicroServices.DataAccess.DoctorsSlots.Interfaces;

namespace MicroServices.DoctorsSlots.Nancy.Interfaces
{
    public interface ISlotForResponse
    {
        int Id { get; set; }
        int DayId { get; set; }
        DateTime EndDateTime { get; set; }
        DateTime StartDateTime { get; set; }
        SlotStatus Status { get; set; }
    }
}