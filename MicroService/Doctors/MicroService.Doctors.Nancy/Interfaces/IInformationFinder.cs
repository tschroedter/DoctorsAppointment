using System.Collections.Generic;
using JetBrains.Annotations;

namespace MicroServices.Doctors.Nancy.Interfaces
{
    public interface IInformationFinder
    {
        IEnumerable <IDoctorForResponse> FindByLastName([NotNull] string lastName);
        IDoctorForResponse FindById(int id);
        IEnumerable <IDoctorForResponse> List();
        IDoctorForResponse Delete(int id);
        IDoctorForResponse Save(IDoctorForResponse doctor);
    }
}