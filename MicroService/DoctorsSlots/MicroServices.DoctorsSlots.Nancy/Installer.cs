using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using MicroServices.DoctorsSlots.Nancy.Interfaces;

namespace MicroServices.DoctorsSlots.Nancy
{
    public sealed class Installer : IWindsorInstaller
    {
        public void Install(IWindsorContainer container,
                            IConfigurationStore store)
        {
            container.Register(Component.For(typeof ( IInformationFinder ))
                                        .ImplementedBy(typeof ( InformationFinder ))
                                        .LifeStyle.Transient);

            container.Register(Component.For(typeof ( IRequestHandler ))
                                        .ImplementedBy(typeof ( RequestHandler ))
                                        .LifeStyle.Transient);
        }
    }
}