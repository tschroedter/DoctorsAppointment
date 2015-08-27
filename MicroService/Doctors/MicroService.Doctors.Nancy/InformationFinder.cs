using System.Collections.Generic;
using System.Linq;
using MicroServices.DataAccess.DoctorsSlots.Interfaces;
using MicroServices.Doctors.Nancy.Interfaces;

namespace MicroServices.Doctors.Nancy
{
    public class InformationFinder : IInformationFinder
    {
        private readonly IDoctorsRepository m_Repository;

        public InformationFinder(IDoctorsRepository repository)
        {
            m_Repository = repository;
        }

        public IEnumerable <IDoctorForResponse> FindByLastName(string lastName)
        {
            IEnumerable <IDoctor> doctors = m_Repository.FindByLastName(lastName);

            DoctorForResponse[] doctorsWithNoSlots = doctors.Select(doctor => new DoctorForResponse(doctor))
                                                            .ToArray();

            return doctorsWithNoSlots;
        }

        public IDoctorForResponse FindById(int id)
        {
            IDoctor doctor = m_Repository.FindById(id);

            return doctor == null
                       ? null
                       : new DoctorForResponse(doctor);
        }

        public IEnumerable <IDoctorForResponse> List()
        {
            IEnumerable <IDoctor> doctors = m_Repository.All;

            DoctorForResponse[] doctorsWithNoSlots = doctors.Select(doctor => new DoctorForResponse(doctor))
                                                            .ToArray();

            return doctorsWithNoSlots;
        }

        public IDoctorForResponse Create(IDoctorForResponse doctor)
        {
            string firstName = string.IsNullOrEmpty(doctor.FirstName)
                                   ? "FirstName"
                                   : doctor.FirstName;
            string lastName = string.IsNullOrEmpty(doctor.FirstName)
                                   ? "LastName"
                                   : doctor.LastName;

            IDoctor created = m_Repository.Create(firstName,
                                                  lastName);

            return created == null
                       ? null
                       : new DoctorForResponse(created);
        }

        public IDoctorForResponse Delete(int id)
        {
            IDoctor doctor = m_Repository.Delete(id);

            return doctor == null
                       ? null
                       : new DoctorForResponse(doctor);
        }
    }
}