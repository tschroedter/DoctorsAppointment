using System.Linq;

namespace MicroServices.DataAccess.DoctorsSlots.Interfaces
{
    public interface IDaysContext : IDbContext <IDay>
    {
        IQueryable <IDay> Days();
    }
}