using System.Collections.Generic;
using JetBrains.Annotations;
using MicroServices.DataAccess.DoctorsSlots.Interfaces;

namespace MicroServices.DoctorsSlots.Nancy.Interfaces
{
    public interface IInformationFinder
    {
        IEnumerable <ISlot> List([NotNull] string doctorLastName,
                                 [CanBeNull] string date,
                                 [CanBeNull] string status);
    }
}