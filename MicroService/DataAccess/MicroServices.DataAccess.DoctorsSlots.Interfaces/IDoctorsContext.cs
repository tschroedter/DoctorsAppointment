using System.Linq;

namespace MicroServices.DataAccess.DoctorsSlots.Interfaces
{
    public interface IDoctorsContext : IDbContext <IDoctor>
    {
        IQueryable <IDoctor> Doctors();
    }
}