using System.Collections.Generic;

namespace MicroServices.DataAccess.DoctorsSlots.Interfaces
{
    public interface IDoctorsSlotsRepository
    {
        IEnumerable <ISlot> FindSlotsForDoctorByDoctorId(int doctorId);
    }
}