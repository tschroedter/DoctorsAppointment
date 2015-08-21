using System;
using System.Linq;
using System.Linq.Expressions;

namespace MicroServices.DataAccess.DoctorsSlots.Interfaces
{
    public interface IRepository <T>
        where T : IEntity
    {
        IQueryable <T> All { get; }
        IQueryable <T> AllIncluding(params Expression <Func <T, object>>[] includeProperties);
        T FindById(int id);
        void AddOrUpdate(T instance);
        void Remove(T entity);
        void Save();
    }
}