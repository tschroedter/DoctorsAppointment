using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using MicroServices.Doctors.Nancy.Interfaces;
using Nancy;
using Nancy.ModelBinding;

namespace MicroServices.Doctors.Nancy
{
    [ExcludeFromCodeCoverage]
    //ncrunch: no coverage start
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

            Get [ "/byLastName/{name:alpha}" ] =
                parameters => handler.FindByLastName(parameters.name);

            Post [ "/" ] =
                parameters => handler.Save(this.Bind <DoctorForResponse>());

            Put [ "/" ] =
                parameters => handler.Save(this.Bind <DoctorForResponse>());

            Delete [ "/{id:int}" ] =
                parameters => handler.DeleteById(( int ) ( parameters.id ));
        }
    }
}