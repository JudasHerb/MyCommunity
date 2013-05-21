using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace MyCommunity.App_Start.Installers
{
    public class ControllerInstaller : IWindsorInstaller
    {
        #region IWindsorInstaller Members

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            var mvcControllerFactory = new WindsorControllerFactory(container.Kernel);
            ControllerBuilder.Current.SetControllerFactory(mvcControllerFactory);

            container.Register(
                AllTypes.FromThisAssembly().BasedOn<IController>().Configure(c => c.LifestyleTransient()));
        }

        #endregion
    }
}