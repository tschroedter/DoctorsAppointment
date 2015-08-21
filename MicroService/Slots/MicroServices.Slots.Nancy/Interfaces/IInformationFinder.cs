using System.Collections.Generic;

namespace MicroServices.Slots.Nancy.Interfaces
{
    public interface IInformationFinder
    {
        ISlotForResponse FindById(int id);
        IEnumerable <ISlotForResponse> List();
    }
}