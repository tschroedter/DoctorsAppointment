using System;
using System.Data.Entity;
using System.Linq;
using MicroServices.DataAccess.DoctorsSlots.Entities;
using MicroServices.DataAccess.DoctorsSlots.Interfaces;

namespace MicroServices.DataAccess.DoctorsSlots.Contexts
{
    public class SlotsContext
        : DbContext,
          ISlotsContext
    {
        public SlotsContext()
            : base("Doctors.Application")
        {
        }

        public DbSet <Slot> DbSetSlots { get; set; }

        public IQueryable <ISlot> Slots()
        {
            return DbSetSlots;
        }

        public void Remove(ISlot slot)
        {
            Slot instance = ConvertToSlot(slot);

            DbSetSlots.Remove(instance);
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

        private static Slot ConvertToSlot(ISlot slot)
        {
            var instance = slot as Slot;

            if ( instance == null )
            {
                throw new ArgumentException("Provided 'slot' instance is not a Slot!",
                                            "slot");
            }
            return instance;
        }
    }
}