using JetBrains.Annotations;
using Nancy;

namespace MicroServices.Doctors.Nancy.Interfaces
{
    public interface IRequestHandler
    {
        Response List();
        Response FindById(int id);
        Response FindByLastName([NotNull] string doctorLastName);
        Response Save([NotNull] IDoctorForResponse doctor);
        Response DeleteById(int id);
    }
}