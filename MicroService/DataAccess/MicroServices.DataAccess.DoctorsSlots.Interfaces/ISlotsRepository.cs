using System.Collections.Generic;

namespace MicroServices.DataAccess.DoctorsSlots.Interfaces
{
    public interface ISlotsRepository
        : IRepository <ISlot>
    {
        IEnumerable <ISlot> FindByDayId(int dayId);
    }
}