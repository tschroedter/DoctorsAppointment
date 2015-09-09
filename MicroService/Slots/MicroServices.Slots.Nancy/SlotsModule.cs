using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using MicroServices.Slots.Nancy.Interfaces;
using Nancy;
using Nancy.ModelBinding;

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

            Post [ "/" ] =
                parameters => handler.Save(this.Bind <SlotForResponse>());

            Put [ "/" ] =
                parameters => handler.Save(this.Bind <SlotForResponse>());

            Delete [ "/{id:int}" ] =
                parameters => handler.DeleteById(( int ) ( parameters.id ));
        }
    }
}