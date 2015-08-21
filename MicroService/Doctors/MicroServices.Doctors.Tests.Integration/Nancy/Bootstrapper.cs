using System.Diagnostics.CodeAnalysis;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using MicroServices.DataAccess.DoctorsSlots.Contexts;
using MicroServices.DataAccess.DoctorsSlots.Interfaces;
using MicroServices.DataAccess.DoctorsSlots.Repositories;
using MicroServices.Doctors.Nancy;
using MicroServices.Doctors.Nancy.Interfaces;
using Nancy.Bootstrappers.Windsor;

namespace MicroServices.Doctors.Tests.Integration.Nancy
{
    [ExcludeFromCodeCoverage]
    //ncrunch: no coverage start
    public class Bootstrapper : WindsorNancyBootstrapper
    {
        protected override void ConfigureApplicationContainer(IWindsorContainer existingContainer)
        {
            base.ConfigureApplicationContainer(existingContainer);

            existingContainer.Register(Component.For(typeof ( IDoctorsContext ))
                                                .ImplementedBy(typeof ( DoctorsContext ))
                                                .LifeStyle.Transient);

            existingContainer.Register(Component.For(typeof ( IInformationFinder ))
                                                .ImplementedBy(typeof ( InformationFinder ))
                                                .LifeStyle.Transient);

            existingContainer.Register(Component.For(typeof ( IDoctorsRepository ))
                                                .ImplementedBy(typeof ( DoctorsRepository ))
                                                .LifeStyle.Transient);

            existingContainer.Register(Component.For(typeof ( IRequestHandler ))
                                                .ImplementedBy(typeof ( RequestHandler ))
                                                .LifeStyle.Transient);
        }
    }
}