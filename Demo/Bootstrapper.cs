﻿using System.Diagnostics.CodeAnalysis;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using Castle.Windsor.Installer;
using MicroServices.DataAccess.DoctorsSlots;
using Nancy.Bootstrappers.Windsor;
using Selkie.Windsor.Installers;

namespace Demo
{
    [ExcludeFromCodeCoverage]
    //ncrunch: no coverage start
    public class Bootstrapper : WindsorNancyBootstrapper
    {
        protected override void ConfigureApplicationContainer(IWindsorContainer existingContainer)
        {
            base.ConfigureApplicationContainer(existingContainer);


            // Add the Array Resolver, so we can take dependencies on T[]
            // while only registering T.
            existingContainer.Kernel.Resolver.AddSubResolver(new ArrayResolver(existingContainer.Kernel));

            var loggerInstaller = new LoggerInstaller();
            loggerInstaller.Install(existingContainer,
                                    null);

            var loaderInstaller = new ProjectComponentLoaderInstaller();
            loaderInstaller.Install(existingContainer,
                                    null);

            existingContainer.Install(FromAssembly.Containing(typeof ( Installer )));
            existingContainer.Install(FromAssembly.Containing(typeof ( MicroServices.Days.Nancy.Installer )));
            existingContainer.Install(FromAssembly.Containing(typeof ( MicroServices.Doctors.Nancy.Installer )));
            existingContainer.Install(FromAssembly.Containing(typeof ( MicroServices.DoctorsSlots.Nancy.Installer )));
            existingContainer.Install(FromAssembly.Containing(typeof ( MicroServices.Slots.Nancy.Installer )));
        }
    }
}