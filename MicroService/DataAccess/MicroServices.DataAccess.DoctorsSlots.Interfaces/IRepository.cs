using System.Linq;

namespace MicroServices.DataAccess.DoctorsSlots.Interfaces
{
    public interface IRepository <T>
        where T : IEntity
    {
        IQueryable <T> All { get; }
        T FindById(int id);
        void Save(T instance);
        void Remove(T entity);
        void Save();
    }
}