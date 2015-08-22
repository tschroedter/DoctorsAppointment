using Nancy;

namespace MicroServices.DoctorsSlots.Nancy.Interfaces
{
    public interface IRequestHandler
    {
        Response List(int doctorId,
                      string date,
                      string status);
    }
}