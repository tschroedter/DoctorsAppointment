using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using JetBrains.Annotations;
using MicroServices.DataAccess.DoctorsSlots.Interfaces;

namespace MicroServices.DataAccess.DoctorsSlots.Repositories
{
    public sealed class SlotsRepository
        : ISlotsRepository
    {
        private readonly ISlotsContext m_Context;

        public SlotsRepository([NotNull] ISlotsContext context)
        {
            m_Context = context;
        }

        public IQueryable <ISlot> All
        {
            get
            {
                return m_Context.Slots();
            }
        }

        public IQueryable <ISlot> AllIncluding(params Expression <Func <ISlot, object>>[] includeProperties)
        {
            IQueryable <ISlot> query = m_Context.Slots();

            foreach ( Expression <Func <ISlot, object>> includeProperty in includeProperties )
            {
                query = query.Include(includeProperty);
            }

            return query;
        }

        public ISlot FindById(int id)
        {
            return m_Context.Find(id);
        }

        public void Remove(ISlot slot)
        {
            m_Context.Remove(slot);
        }

        public void Save()
        {
            m_Context.SaveChanges();
        }

        public void AddOrUpdate(ISlot slot)
        {
            if ( slot.Id == default ( int ) )
            {
                m_Context.Add(slot);
            }
            else
            {
                m_Context.SetStateForSlot(slot,
                                          EntityState.Modified);
            }

            m_Context.Add(slot);
        }
    }
}