using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using MicroServices.Slots.Nancy.Interfaces;
using Nancy;

namespace MicroServices.Slots.Nancy
{
    [ExcludeFromCodeCoverage]
    //ncrunch: no coverage start
    public class SlotsModule
        : NancyModule
    {
        public SlotsModule([NotNull] IRequestHandler handler)
            : base("/slots")
        {
            Get [ "/" ] =
                parameters => handler.List();

            Get [ "/{id:int}" ] =
                parameters => handler.FindById(( int ) ( parameters.id ));
        }
    }
}