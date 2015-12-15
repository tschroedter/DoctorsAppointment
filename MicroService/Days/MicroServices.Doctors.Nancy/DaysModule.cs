using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using MicroServices.Days.Nancy.Interfaces;
using Nancy;
using Nancy.ModelBinding;

namespace MicroServices.Days.Nancy
{
    [ExcludeFromCodeCoverage]
    //ncrunch: no coverage start
    public class DaysModule
        : NancyModule
    {
        public DaysModule([NotNull] IRequestHandler handler)
            : base("/days")
        {
            Get [ "/" ] =
                parameters => handler.List();

            Get [ "/{id:int}" ] =
                parameters => handler.FindById(( int ) parameters.id);

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

            // todo testing for all below
            Get [ "/doctorId/{doctorid:int}" ] =
                parameters => handler.FindByDoctorId(( int ) parameters.doctorid);

            Post [ "/" ] =
                parameters => handler.Save(this.Bind <DayForResponse>());

            Put [ "/" ] =
                parameters => handler.Save(this.Bind <DayForResponse>());

            Delete [ "/{id:int}" ] =
                parameters => handler.DeleteById(( int ) parameters.id);
        }
    }
}