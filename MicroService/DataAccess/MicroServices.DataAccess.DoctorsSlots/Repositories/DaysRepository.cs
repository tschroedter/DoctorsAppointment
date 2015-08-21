using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using JetBrains.Annotations;
using MicroServices.DataAccess.DoctorsSlots.Interfaces;

namespace MicroServices.DataAccess.DoctorsSlots.Repositories
{
    public sealed class DaysRepository
        : IDaysRepository
    {
        private readonly IDaysContext m_Context;

        public DaysRepository([NotNull] IDaysContext context)
        {
            m_Context = context;
        }

        public IQueryable <IDay> All
        {
            get
            {
                return m_Context.Days();
            }
        }

        public IQueryable <IDay> AllIncluding(params Expression <Func <IDay, object>>[] includeProperties)
        {
            IQueryable <IDay> query = m_Context.Days();

            foreach ( Expression <Func <IDay, object>> includeProperty in includeProperties )
            {
                query = query.Include(includeProperty);
            }

            return query;
        }

        public IDay FindById(int id)
        {
            return m_Context.Find(id);
        }

        public void AddOrUpdate(IDay doctor)
        {
            if ( doctor.Id == default ( int ) )
            {
                m_Context.Add(doctor);
            }
            else
            {
                m_Context.SetStateForSlot(doctor,
                                          EntityState.Modified);
            }

            m_Context.Add(doctor);
        }

        public IEnumerable <IDay> FindByDoctorId(int doctorId)
        {
            IQueryable <IDay> days = m_Context.Days()
                                              .Where(x => x.DoctorId == doctorId);

            return days;
        }

        // todo testing 
        public IEnumerable <IDay> FindByDate(DateTime dateTime)
        {
            IDay[] days = m_Context.Days().ToArray();

            IEnumerable <IDay> forDateTime = days.Where(x => x.Date.Date == dateTime.Date);

            return forDateTime;
        }

        public void Save()
        {
            m_Context.SaveChanges();
        }

        public void Remove(IDay doctor)
        {
            m_Context.Remove(doctor);
        }
    }
}