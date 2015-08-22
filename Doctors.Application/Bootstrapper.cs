using System.Diagnostics.CodeAnalysis;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using MicroServices.DataAccess.DoctorsSlots.Contexts;
using MicroServices.DataAccess.DoctorsSlots.Interfaces;
using MicroServices.DataAccess.DoctorsSlots.Repositories;
using MicroServices.Doctors.Nancy;
using MicroServices.Doctors.Nancy.Interfaces;
using Nancy.Bootstrappers.Windsor;

namespace Doctors.Application
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

            existingContainer.Register(Component.For(typeof ( ISlotsContext ))
                                                .ImplementedBy(typeof ( SlotsContext ))
                                                .LifeStyle.Transient);

            existingContainer.Register(Component.For(typeof ( MicroServices.Slots.Nancy.Interfaces.IInformationFinder ))
                                                .ImplementedBy(typeof ( MicroServices.Slots.Nancy.InformationFinder ))
                                                .LifeStyle.Transient);

            existingContainer.Register(Component.For(typeof ( ISlotsRepository ))
                                                .ImplementedBy(typeof ( SlotsRepository ))
                                                .LifeStyle.Transient);

            existingContainer.Register(Component.For(typeof ( MicroServices.Slots.Nancy.Interfaces.IRequestHandler ))
                                                .ImplementedBy(typeof ( MicroServices.Slots.Nancy.RequestHandler ))
                                                .LifeStyle.Transient);

            existingContainer.Register(Component.For(typeof ( IDoctorsSlotsContext ))
                                                .ImplementedBy(typeof ( DoctorsSlotsContext ))
                                                .LifeStyle.Transient);

            existingContainer.Register(
                                       Component.For(
                                                     typeof (
                                                         MicroServices.DoctorsSlots.Nancy.Interfaces.IInformationFinder
                                                         ))
                                                .ImplementedBy(
                                                               typeof (
                                                                   MicroServices.DoctorsSlots.Nancy.InformationFinder ))
                                                .LifeStyle.Transient);

            existingContainer.Register(Component.For(typeof ( IDoctorsSlotsRepository ))
                                                .ImplementedBy(typeof ( DoctorsSlotsRepository ))
                                                .LifeStyle.Transient);

            existingContainer.Register(
                                       Component.For(
                                                     typeof (
                                                         MicroServices.DoctorsSlots.Nancy.Interfaces.IRequestHandler ))
                                                .ImplementedBy(
                                                               typeof ( MicroServices.DoctorsSlots.Nancy.RequestHandler
                                                                   ))
                                                .LifeStyle.Transient);

            existingContainer.Register(Component.For(typeof ( IDaysContext ))
                                                .ImplementedBy(typeof ( DaysContext ))
                                                .LifeStyle.Transient);

            existingContainer.Register(Component.For(typeof ( MicroServices.Days.Nancy.Interfaces.IInformationFinder ))
                                                .ImplementedBy(typeof ( MicroServices.Days.Nancy.InformationFinder ))
                                                .LifeStyle.Transient);

            existingContainer.Register(Component.For(typeof ( IDaysRepository ))
                                                .ImplementedBy(typeof ( DaysRepository ))
                                                .LifeStyle.Transient);

            existingContainer.Register(Component.For(typeof ( MicroServices.Days.Nancy.Interfaces.IRequestHandler ))
                                                .ImplementedBy(typeof ( MicroServices.Days.Nancy.RequestHandler ))
                                                .LifeStyle.Transient);
        }
    }
}