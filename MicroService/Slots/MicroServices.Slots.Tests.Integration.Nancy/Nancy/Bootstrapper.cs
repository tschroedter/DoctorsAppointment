﻿using System.Diagnostics.CodeAnalysis;
using Castle.Windsor;
using Castle.Windsor.Installer;
using MicroServices.DataAccess.DoctorsSlots;
using Nancy.Bootstrappers.Windsor;
using Selkie.Windsor.Installers;

namespace MicroServices.Slots.Tests.Integration.Nancy.Nancy
{
    [ExcludeFromCodeCoverage]
    //ncrunch: no coverage start
    public class Bootstrapper : WindsorNancyBootstrapper
    {
        protected override void ConfigureApplicationContainer(IWindsorContainer existingContainer)
        {
            base.ConfigureApplicationContainer(existingContainer);

            var loggerInstaller = new LoggerInstaller();
            loggerInstaller.Install(existingContainer,
                                    null);

            var loaderInstaller = new ProjectComponentLoaderInstaller();
            loaderInstaller.Install(existingContainer,
                                    null);

            existingContainer.Install(FromAssembly.Containing(typeof ( Installer )));
            existingContainer.Install(FromAssembly.Containing(typeof ( Slots.Nancy.Installer )));
        }
    }
}