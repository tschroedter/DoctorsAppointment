using System.Collections.Generic;
using JetBrains.Annotations;

namespace MicroServices.Doctors.Nancy.Interfaces
{
    public interface IInformationFinder
    {
        IEnumerable <IDoctorForResponse> FindByLastName([NotNull]string lastName);
        IDoctorForResponse FindById(int id);
        IEnumerable <IDoctorForResponse> List();
        IDoctorForResponse Create([NotNull] IDoctorForResponse doctor);
        IDoctorForResponse Delete(int id);
    }
}