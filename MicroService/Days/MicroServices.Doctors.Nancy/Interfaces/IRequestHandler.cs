using Nancy;

namespace MicroServices.Days.Nancy.Interfaces
{
    public interface IRequestHandler
    {
        Response List();
        Response FindById(int id);

        Response Find(string dayId,
                      string doctorId);

        Response FindByDate(string date);
    }
}