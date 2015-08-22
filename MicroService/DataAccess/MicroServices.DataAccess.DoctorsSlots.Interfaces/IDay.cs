using System;
using System.Collections.Generic;

namespace MicroServices.DataAccess.DoctorsSlots.Interfaces
{
    public interface IDay : IEntity
    {
        DateTime Date { get; set; }
        int DoctorId { get; set; }
        IEnumerable <ISlot> AppointmentSlots();
    }
}