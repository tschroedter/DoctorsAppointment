using JetBrains.Annotations;
using MicroServices.Doctors.Nancy.Interfaces;
using Nancy;

namespace MicroServices.Doctors.Nancy
{
    public class DoctorsModule
        : NancyModule
    {
        public DoctorsModule([NotNull] IRequestHandler handler)
            : base("/doctors")
        {
            Get [ "/" ] =
                parameters => handler.List();

            Get [ "/{id:int}" ] =
                parameters => handler.FindById(( int ) ( parameters.id ));

            Get [ "/{name:alpha}" ] =
                parameters => handler.FindByLastName(parameters.name);
        }
    }
}