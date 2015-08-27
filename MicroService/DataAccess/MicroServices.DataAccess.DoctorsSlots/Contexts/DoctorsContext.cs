using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using MicroServices.DataAccess.DoctorsSlots.Entities;
using MicroServices.DataAccess.DoctorsSlots.Interfaces;

namespace MicroServices.DataAccess.DoctorsSlots.Contexts
{
    [ExcludeFromCodeCoverage]
    //ncrunch: no coverage start
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

        public new void SaveChanges()
        {
            base.SaveChanges();
        }

        public IDoctor Find(int id)
        {
            return DbSetDoctors.Find(id);
        }

        public IDoctor Create()
        {
            var instance = new Doctor
                           {
                               LastName = "LastName",
                               FirstName = "FirstName"
                           };

            Add(instance);
            SaveChanges();

            return instance;

            /* todo this code should be an aspect (AOP)
            try
            {
                SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}",
                                                validationError.PropertyName,
                                                validationError.ErrorMessage);
                    }
                }
            }
            */
        }

        public IDoctor Delete(int id)
        {
            Doctor instance = DbSetDoctors.Find(id);

            if ( instance == null )
            {
                return null;
            }

            Remove(instance);
            SaveChanges();

            return instance;
        }

        public void Add(IDoctor doctor)
        {
            Doctor instance = ConvertToSlot(doctor);

            DbSetDoctors.AddOrUpdate(instance);
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