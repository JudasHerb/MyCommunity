using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace MyCommunity.App_Start.Installers
{
    public class ServicesInstaller : IWindsorInstaller
    {
        #region IWindsorInstaller Members

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Classes.FromThisAssembly().InNamespace("MyCommunity.Services").WithServiceFirstInterface().Configure(s=>s.LifestyleTransient()));
        }

        #endregion
    }
}