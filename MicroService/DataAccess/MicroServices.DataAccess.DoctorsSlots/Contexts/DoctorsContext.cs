using System;
using System.Data.Entity;
using System.Linq;
using MicroServices.DataAccess.DoctorsSlots.Entities;
using MicroServices.DataAccess.DoctorsSlots.Interfaces;

namespace MicroServices.DataAccess.DoctorsSlots.Contexts
{
    public class DoctorsContext
        : DbContext,
          IDoctorsContext
    {
        public DoctorsContext()
            : base("Doctors.Application")
        {
        }

        public DbSet <Doctor> DbSetDoctors { get; set; }

        public IQueryable <IDoctor> Doctors()
        {
            return DbSetDoctors;
        }

        public void Remove(IDoctor doctor)
        {
            Doctor instance = ConvertToSlot(doctor);

            DbSetDoctors.Remove(instance);
        }

        public new void SaveChanges() // todo check why new???
        {
            base.SaveChanges();
        }

        public IDoctor Find(int id)
        {
            return DbSetDoctors.Find(id);
        }

        public void Add(IDoctor doctor)
        {
            Doctor instance = ConvertToSlot(doctor);

            DbSetDoctors.Add(instance);
        }

        public void SetStateForSlot(IDoctor doctor,
                                    EntityState modified)
        {
            Doctor instance = ConvertToSlot(doctor);

            Entry(instance).State = EntityState.Modified;
        }

        private static Doctor ConvertToSlot(IDoctor doctor)
        {
            var instance = doctor as Doctor;

            if ( instance == null )
            {
                throw new ArgumentException("Provided 'doctor' instance is not a Doctor!",
                                            "doctor");
            }
            return instance;
        }
    }
}