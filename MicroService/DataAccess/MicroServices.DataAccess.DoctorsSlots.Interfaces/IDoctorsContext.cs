using System.Data.Entity;
using System.Linq;
using JetBrains.Annotations;

namespace MicroServices.DataAccess.DoctorsSlots.Interfaces
{
    public interface IDoctorsContext
    {
        IQueryable <IDoctor> Doctors();
        void Remove([NotNull] IDoctor doctor);
        IDoctor Find(int id);
        void Add([NotNull] IDoctor doctor);
        int SaveChanges();

        void SetStateForSlot([NotNull] IDoctor doctor,
                             EntityState modified);
    }
}