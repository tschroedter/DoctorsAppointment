using System.Collections.Generic;

namespace MicroServices.Doctors.Nancy.Interfaces
{
    public interface IInformationFinder
    {
        IEnumerable <IDoctorForResponse> FindByLastName(string lastName);
        IDoctorForResponse FindById(int id);
        IEnumerable <IDoctorForResponse> List();
        IDoctorForResponse Create();
        IDoctorForResponse Delete(int id);
    }
}