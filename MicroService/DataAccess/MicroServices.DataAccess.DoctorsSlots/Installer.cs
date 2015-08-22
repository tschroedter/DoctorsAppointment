using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using MicroServices.DataAccess.DoctorsSlots.Contexts;
using MicroServices.DataAccess.DoctorsSlots.Interfaces;
using MicroServices.DataAccess.DoctorsSlots.Repositories;

namespace MicroServices.DataAccess.DoctorsSlots
{
    public sealed class Installer : IWindsorInstaller
    {
        public void Install(IWindsorContainer container,
                            IConfigurationStore store)
        {
            container.Register(Component.For(typeof ( IDoctorsContext ))
                                        .ImplementedBy(typeof ( DoctorsContext ))
                                        .LifeStyle.Transient);

            container.Register(Component.For(typeof ( IDoctorsRepository ))
                                        .ImplementedBy(typeof ( DoctorsRepository ))
                                        .LifeStyle.Transient);

            container.Register(Component.For(typeof ( ISlotsContext ))
                                        .ImplementedBy(typeof ( SlotsContext ))
                                        .LifeStyle.Transient);

            container.Register(Component.For(typeof ( ISlotsRepository ))
                                        .ImplementedBy(typeof ( SlotsRepository ))
                                        .LifeStyle.Transient);

            container.Register(Component.For(typeof ( IDoctorsSlotsContext ))
                                        .ImplementedBy(typeof ( DoctorsSlotsContext ))
                                        .LifeStyle.Transient);

            container.Register(Component.For(typeof ( IDoctorsSlotsRepository ))
                                        .ImplementedBy(typeof ( DoctorsSlotsRepository ))
                                        .LifeStyle.Transient);

            container.Register(Component.For(typeof ( IDaysContext ))
                                        .ImplementedBy(typeof ( DaysContext ))
                                        .LifeStyle.Transient);

            container.Register(Component.For(typeof ( IDaysRepository ))
                                        .ImplementedBy(typeof ( DaysRepository ))
                                        .LifeStyle.Transient);
        }
    }
}