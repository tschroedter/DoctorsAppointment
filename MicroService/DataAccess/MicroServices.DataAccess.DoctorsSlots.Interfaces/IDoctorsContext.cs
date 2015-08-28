using System.Linq;
using JetBrains.Annotations;

namespace MicroServices.DataAccess.DoctorsSlots.Interfaces
{
    public interface IDoctorsContext : IDbContext <IDoctor>
    {
        IQueryable <IDoctor> Doctors();

        IDoctor Create([NotNull] string firstName,
                       [NotNull] string lastName);

        IDoctor Delete(int id);
    }
}