using System.Collections.Generic;
using MicroServices.DataAccess.DoctorsSlots.Interfaces;

namespace MicroServices.DoctorsSlots.Nancy.Interfaces
{
    public interface IInformationFinder
    {
        IEnumerable <ISlot> List(int doctorId,
                                 string date,
                                 string status);
    }
}