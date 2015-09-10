using System.Collections.Generic;
using System.Linq;
using MicroServices.DataAccess.DoctorsSlots.Entities;
using MicroServices.DataAccess.DoctorsSlots.Interfaces;
using MicroServices.Doctors.Nancy.Interfaces;
using Selkie.Windsor;

namespace MicroServices.Doctors.Nancy
{
    [ProjectComponent(Lifestyle.Transient)]
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

        public IDoctorForResponse Delete(int id)
        {
            IDoctor doctor = m_Repository.Delete(id);

            return doctor == null
                       ? null
                       : new DoctorForResponse(doctor);
        }

        public IDoctorForResponse Save(IDoctorForResponse doctor)
        {
            IDoctor toBeUpdated = ToDoctor(doctor);

            m_Repository.Save(toBeUpdated);

            return new DoctorForResponse(toBeUpdated);
        }

        private static IDoctor ToDoctor(IDoctorForResponse doctor)
        {
            string firstName = DefaultText(doctor.FirstName,
                                           "FirstName");
            string lastName = DefaultText(doctor.LastName,
                                          "LastName");

            IDoctor instance = new Doctor
                               {
                                   Id = doctor.Id,
                                   FirstName = firstName,
                                   LastName = lastName
                               };

            return instance;
        }

        private static string DefaultText(string text,
                                          string defaultText)
        {
            return string.IsNullOrEmpty(text)
                       ? defaultText
                       : text;
        }
    }
}