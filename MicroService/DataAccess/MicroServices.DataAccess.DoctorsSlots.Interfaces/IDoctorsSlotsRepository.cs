using System.Collections.Generic;
using JetBrains.Annotations;

namespace MicroServices.DataAccess.DoctorsSlots.Interfaces
{
    public interface IDoctorsSlotsRepository
    {
        IEnumerable <ISlot> FindSlotsForDoctorByLastName([NotNull] string doctorLastName);
    }
}