using System.Data.Entity;
using System.Linq;
using JetBrains.Annotations;

namespace MicroServices.DataAccess.DoctorsSlots.Interfaces
{
    public interface ISlotsContext
    {
        IQueryable <ISlot> Slots();
        void Remove([NotNull] ISlot slot);
        ISlot Find(int id);
        void Add([NotNull] ISlot slot);
        int SaveChanges();

        void SetStateForSlot([NotNull] ISlot slot,
                             EntityState modified);
    }
}