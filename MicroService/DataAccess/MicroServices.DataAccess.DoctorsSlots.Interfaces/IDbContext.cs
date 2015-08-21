using System.Data.Entity;

namespace MicroServices.DataAccess.DoctorsSlots.Interfaces
{
    public interface IDbContext <T>
    {
        void Add(T instance);
        T Find(int id);
        void Remove(T instance);
        void SaveChanges();

        void SetStateForSlot(T instance,
                             EntityState state);
    }
}