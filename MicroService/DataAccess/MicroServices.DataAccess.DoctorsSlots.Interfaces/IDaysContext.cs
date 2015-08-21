using System.Data.Entity;
using System.Linq;
using JetBrains.Annotations;

namespace MicroServices.DataAccess.DoctorsSlots.Interfaces
{
    public interface IDaysContext
    {
        IQueryable <IDay> Days();
        void Remove([NotNull] IDay doctor);
        IDay Find(int id);
        void Add([NotNull] IDay doctor);
        int SaveChanges();

        void SetStateForSlot([NotNull] IDay doctor,
                             EntityState modified);
    }
}