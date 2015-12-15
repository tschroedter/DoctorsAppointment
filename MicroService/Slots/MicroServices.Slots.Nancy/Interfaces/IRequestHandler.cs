using Nancy;

namespace MicroServices.Slots.Nancy.Interfaces
{
    public interface IRequestHandler
    {
        Response List();
        Response FindById(int id);
        Response Save(ISlotForResponse slot);
        Response DeleteById(int i);
        Response FindByDayId(int dayId);
    }
}