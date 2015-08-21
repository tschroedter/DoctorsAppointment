using System.Collections.Generic;

namespace MicroServices.DataAccess.DoctorsSlots.Interfaces
{
    public interface IDoctor : IEntity
    {
        string FirstName { get; set; }
        string LastName { get; set; }
        IEnumerable <IDay> AppointmentDays { get; }
    }
}