using System.Collections.Generic;

namespace MicroServices.Days.Nancy.Interfaces
{
    public interface IInformationFinder
    {
        IEnumerable <IDayForResponse> FindByDoctorId(int doctorId);
        IDayForResponse FindById(int id);
        IEnumerable <IDayForResponse> List();
        IEnumerable <IDayForResponse> ListForDate(string date);

        IEnumerable <IDayForResponse> ListForDateAndDoctorId(string date,
                                                             string doctorId);

        IDayForResponse Save(IDayForResponse day);
        IDayForResponse Delete(int id);
    }
}