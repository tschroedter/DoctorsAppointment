using System.Diagnostics.CodeAnalysis;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using MicroServices.DataAccess.DoctorsSlots.Contexts;
using MicroServices.DataAccess.DoctorsSlots.Interfaces;
using MicroServices.DataAccess.DoctorsSlots.Repositories;
using MicroServices.Slots.Nancy;
using MicroServices.Slots.Nancy.Interfaces;
using Nancy.Bootstrappers.Windsor;

namespace MicroServices.Slots.Tests.Integration.Nancy.Nancy
{
    [ExcludeFromCodeCoverage]
    //ncrunch: no coverage start
    public class Bootstrapper : WindsorNancyBootstrapper
    {
        protected override void ConfigureApplicationContainer(IWindsorContainer existingContainer)
        {
            base.ConfigureApplicationContainer(existingContainer);

            existingContainer.Register(Component.For(typeof ( ISlotsContext ))
                                                .ImplementedBy(typeof ( SlotsContext ))
                                                .LifeStyle.Transient);

            existingContainer.Register(Component.For(typeof ( IInformationFinder ))
                                                .ImplementedBy(typeof ( InformationFinder ))
                                                .LifeStyle.Transient);

            existingContainer.Register(Component.For(typeof ( ISlotsRepository ))
                                                .ImplementedBy(typeof ( SlotsRepository ))
                                                .LifeStyle.Transient);

            existingContainer.Register(Component.For(typeof ( IRequestHandler ))
                                                .ImplementedBy(typeof ( RequestHandler ))
                                                .LifeStyle.Transient);
        }
    }
}