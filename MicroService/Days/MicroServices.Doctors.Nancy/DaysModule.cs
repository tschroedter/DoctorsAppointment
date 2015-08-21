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

            Get [ "/{date:datetime(yyyy-MM-dd)}" ] =
                parameters =>
                {
                    string date = parameters.date;

                    return handler.FindByDate(date);
                };

            Get [ "/{date:datetime(yyyy-MM-dd)}/doctors" ] =
                parameters =>
                {
                    string doctorId = Request.Query.doctorId;
                    string date = parameters.date;

                    return handler.Find(date,
                                        doctorId);
                };
        }
    }
}