using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using MicroServices.DoctorsSlots.Nancy.Interfaces;
using Nancy;

namespace MicroServices.DoctorsSlots.Nancy
{
    [ExcludeFromCodeCoverage]
    //ncrunch: no coverage start
    public class DoctorsSlotsModule
        : NancyModule
    {
        public DoctorsSlotsModule([NotNull] IRequestHandler handler)
            : base("/doctors")
        {
            Get [ "/{name:alpha}/slots" ] =
                parameters =>
                {
                    string date = Request.Query.date;
                    string status = Request.Query.status;
                    string doctorLastName = parameters.name;

                    return handler.List(doctorLastName,
                                        date,
                                        status);
                };
        }
    }
}