using Nancy;

namespace MicroServices.Doctors.Nancy.Interfaces
{
    public interface IRequestHandler
    {
        Response List();
        Response FindById(int id);
        Response FindByLastName(string doctorLastName);
        Response Create();
        Response DeleteById(int id);
    }
}