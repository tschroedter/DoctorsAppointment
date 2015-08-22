using System.Linq;

namespace MicroServices.DataAccess.DoctorsSlots.Interfaces
{
    public interface ISlotsContext : IDbContext <ISlot>
    {
        IQueryable <ISlot> Slots();
    }
}