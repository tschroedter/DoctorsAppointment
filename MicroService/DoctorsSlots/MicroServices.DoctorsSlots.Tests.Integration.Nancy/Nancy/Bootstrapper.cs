using Castle.MicroKernel.Registration;
using Castle.Windsor;
using MicroServices.DataAccess.DoctorsSlots.Contexts;
using MicroServices.DataAccess.DoctorsSlots.Interfaces;
using MicroServices.DataAccess.DoctorsSlots.Repositories;
using MicroServices.DoctorsSlots.Nancy;
using MicroServices.DoctorsSlots.Nancy.Interfaces;
using Nancy.Bootstrappers.Windsor;

namespace MicroServices.DoctorsSlots.Tests.Integration.Nancy.Nancy
{
    public class Bootstrapper : WindsorNancyBootstrapper
    {
        protected override void ConfigureApplicationContainer(IWindsorContainer existingContainer)
        {
            base.ConfigureApplicationContainer(existingContainer);

            existingContainer.Register(Component.For(typeof ( IDoctorsSlotsContext ))
                                                .ImplementedBy(typeof ( DoctorsSlotsContext ))
                                                .LifeStyle.Transient);

            existingContainer.Register(Component.For(typeof ( IInformationFinder ))
                                                .ImplementedBy(typeof ( InformationFinder ))
                                                .LifeStyle.Transient);

            existingContainer.Register(Component.For(typeof ( IDoctorsSlotsRepository ))
                                                .ImplementedBy(typeof ( DoctorsSlotsRepository ))
                                                .LifeStyle.Transient);

            existingContainer.Register(Component.For(typeof ( IRequestHandler ))
                                                .ImplementedBy(typeof ( RequestHandler ))
                                                .LifeStyle.Transient);

            existingContainer.Register(Component.For(typeof ( IDoctorsContext ))
                                                .ImplementedBy(typeof ( DoctorsContext ))
                                                .LifeStyle.Transient);

            existingContainer.Register(Component.For(typeof ( IDoctorsRepository ))
                                                .ImplementedBy(typeof ( DoctorsRepository ))
                                                .LifeStyle.Transient);
        }
    }
}