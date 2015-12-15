using System.Collections.Generic;

namespace MicroServices.Slots.Nancy.Interfaces
{
    public interface IInformationFinder
    {
        ISlotForResponse FindById(int id);
        IEnumerable <ISlotForResponse> List();
        ISlotForResponse Save(ISlotForResponse slot);
        ISlotForResponse Delete(int id);
        IEnumerable <ISlotForResponse> FindByDayId(int dayId);
    }
}