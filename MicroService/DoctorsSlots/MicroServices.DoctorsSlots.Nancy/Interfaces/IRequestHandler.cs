using Nancy;

namespace MicroServices.DoctorsSlots.Nancy.Interfaces
{
    public interface IRequestHandler
    {
        Response List(string doctorLastName,
                      string date,
                      string status);
    }
}