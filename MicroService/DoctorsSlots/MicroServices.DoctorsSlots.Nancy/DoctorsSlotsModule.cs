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
            Get [ "/{doctorId:int}/slots" ] =
                parameters =>
                {
                    string date = Request.Query.date;
                    string status = Request.Query.status;
                    int doctorLastName = parameters.doctorId;

                    return handler.List(doctorLastName,
                                        date,
                                        status);
                };
        }
    }
}