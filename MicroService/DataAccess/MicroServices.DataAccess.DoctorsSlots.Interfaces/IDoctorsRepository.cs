using System.Collections.Generic;
using JetBrains.Annotations;

namespace MicroServices.DataAccess.DoctorsSlots.Interfaces
{
    public interface IDoctorsRepository
        : IRepository <IDoctor>
    {
        IEnumerable <IDoctor> FindByLastName([NotNull] string lastName);
        IDoctor Delete(int id);
    }
}