using Nancy;

namespace MicroServices.Slots.Nancy.Interfaces
{
    public interface IRequestHandler
    {
        Response List();
        Response FindById(int id);
    }
}