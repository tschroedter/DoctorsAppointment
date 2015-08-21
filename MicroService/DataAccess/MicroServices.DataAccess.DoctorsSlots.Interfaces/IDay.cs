using System;

namespace MicroServices.DataAccess.DoctorsSlots.Interfaces
{
    public interface IDay : IEntity
    {
        DateTime Date { get; set; }
        int DoctorId { get; set; }
    }
}