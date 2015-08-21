using JetBrains.Annotations;
using MicroServices.Days.Nancy.Interfaces;
using Nancy;

namespace MicroServices.Days.Nancy
{
    public class DaysModule
        : NancyModule
    {
        public DaysModule([NotNull] IRequestHandler handler)
            : base("/days")
        {
            Get [ "/" ] =
                parameters => handler.List();

            Get [ "/{id:int}" ] =
                parameters => handler.FindById(( int ) ( parameters.id ));

            // todo testing
            Get [ "/{date:datetime(yyyy-MM-dd)}" ] =
                parameters =>
                {
                    string date = parameters.date;

                    return handler.FindByDate(date);
                };

            // todo doesn't work
            Get [ "/{date:alpha}/doctors" ] =
                parameters =>
                {
                    int doctorId = Request.Query.doctorId;
                    string date = parameters.date;

                    return handler.Find(date,
                                        doctorId);
                };
        }
    }
}