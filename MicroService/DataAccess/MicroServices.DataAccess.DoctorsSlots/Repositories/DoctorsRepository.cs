using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using JetBrains.Annotations;
using MicroServices.DataAccess.DoctorsSlots.Interfaces;

namespace MicroServices.DataAccess.DoctorsSlots.Repositories
{
    public sealed class DoctorsRepository
        : IDoctorsRepository
    {
        private readonly IDoctorsContext m_Context;

        public DoctorsRepository([NotNull] IDoctorsContext context)
        {
            m_Context = context;
        }

        public IQueryable <IDoctor> All
        {
            get
            {
                return m_Context.Doctors();
            }
        }

        public IQueryable <IDoctor> AllIncluding(params Expression <Func <IDoctor, object>>[] includeProperties)
        {
            IQueryable <IDoctor> query = m_Context.Doctors();

            foreach ( Expression <Func <IDoctor, object>> includeProperty in includeProperties )
            {
                query = query.Include(includeProperty);
            }

            return query;
        }

        public IDoctor FindById(int id)
        {
            return m_Context.Find(id);
        }

        public void AddOrUpdate(IDoctor doctor)
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

        public IEnumerable <IDoctor> FindByLastName(string lastName)
        {
            IQueryable <IDoctor> doctor = m_Context.Doctors().Where(x => x.LastName == lastName);

            return doctor;
        }

        public void Save()
        {
            m_Context.SaveChanges();
        }

        public void Remove(IDoctor doctor)
        {
            m_Context.Remove(doctor);
        }
    }
}