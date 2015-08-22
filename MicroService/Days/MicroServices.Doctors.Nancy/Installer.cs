using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using MicroServices.Days.Nancy.Interfaces;

namespace MicroServices.Days.Nancy
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