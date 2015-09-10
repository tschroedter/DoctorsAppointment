using System;
using System.Data.Entity;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using MicroServices.DataAccess.DoctorsSlots.Entities;
using MicroServices.DataAccess.DoctorsSlots.Interfaces;
using Selkie.Windsor;

namespace MicroServices.DataAccess.DoctorsSlots.Contexts
{
    [ExcludeFromCodeCoverage]
    //ncrunch: no coverage start
    [ProjectComponent(Lifestyle.Transient)]
    public class DaysContext
        : DbContext,
          IDaysContext
    {
        public DaysContext()
            : base("Doctors.Application")
        {
        }

        public DbSet <Day> DbSetDays { get; set; }

        public IQueryable <IDay> Days()
        {
            return DbSetDays;
        }

        public void Remove(IDay day)
        {
            Day instance = ConvertToDay(day);

            DbSetDays.Remove(instance);
        }

        public new void SaveChanges()
        {
            base.SaveChanges();
        }

        public IDay Find(int id)
        {
            return DbSetDays.Find(id);
        }

        public void Add(IDay doctor)
        {
            Day instance = ConvertToDay(doctor);

            DbSetDays.Add(instance);
        }

        public void SetStateForSlot(IDay day,
                                    EntityState modified)
        {
            Day instance = ConvertToDay(day);

            Entry(instance).State = EntityState.Modified;
        }

        private static Day ConvertToDay(IDay day)
        {
            var instance = day as Day;

            if ( instance == null )
            {
                throw new ArgumentException("Provided 'day' instance is not a Day!",
                                            "day");
            }
            return instance;
        }
    }
}