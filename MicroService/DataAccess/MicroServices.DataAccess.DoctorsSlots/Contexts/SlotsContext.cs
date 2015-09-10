using System;
using System.Data.Entity;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using MicroServices.DataAccess.DoctorsSlots.Entities;
using MicroServices.DataAccess.DoctorsSlots.Interfaces;
using Selkie.Windsor;

namespace MicroServices.DataAccess.DoctorsSlots.Contexts
{
    // todo need to catch/handle exceptions
    [ExcludeFromCodeCoverage]
    //ncrunch: no coverage start
    [ProjectComponent(Lifestyle.Transient)]
    public class SlotsContext
        : DbContext,
          ISlotsContext
    {
        public SlotsContext()
            : base("Doctors.Application")
        {
        }

        public DbSet <Slot> DbSetSlots { get; set; }

        public DbSet <Day> DbSetDays { get; set; }

        public IQueryable <ISlot> Slots()
        {
            return DbSetSlots;
        }

        public void Remove(ISlot slot)
        {
            Slot instance = ConvertToSlot(slot);

            DbSetSlots.Remove(instance);
        }

        public new void SaveChanges()
        {
            base.SaveChanges();
        }

        public ISlot Find(int id)
        {
            return DbSetSlots.Find(id);
        }

        public void Add(ISlot slot)
        {
            Slot instance = ConvertToSlot(slot);

            DbSetSlots.Add(instance);
        }

        public void SetStateForSlot(ISlot slot,
                                    EntityState modified)
        {
            Slot instance = ConvertToSlot(slot);

            Entry(instance).State = EntityState.Modified;
        }

        private Slot ConvertToSlot(ISlot slot)
        {
            var instance = slot as Slot;

            if ( instance == null )
            {
                throw new ArgumentException("Provided 'slot' instance is not a Slot!",
                                            "slot");
            }

            Day day = DbSetDays.Find(slot.DayId);

            instance.Day = day;

            return instance;
        }
    }
}