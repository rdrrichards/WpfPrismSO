using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using Prism.Logging;
using Prism.Modularity;
using Prism.Unity;
using System.Windows;
using WpfPrismSO.Modules;
using WpfPrismSO.Services;

namespace WpfPrismSO
{
    public class Bootstrapper : UnityBootstrapper
    {
        // The following run basically in this order
        protected override IModuleCatalog CreateModuleCatalog()
        {
            ModuleCatalog catalog = new ModuleCatalog();

            catalog.AddModule(typeof(Module1));

            return catalog;
        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            InitContainer();

            //Unity.Container = Container;
        }

        private void InitContainer()
        {
            RegisterTypeIfMissing(typeof(IPoller), typeof(SimplePoller), true);
            //Container.RegisterInstance(typeof(IPoller), "poller", new SimplePoller(), new ContainerControlledLifetimeManager());
        }

        protected override DependencyObject CreateShell()
        {
            return ServiceLocator.Current.GetInstance<MainWindow>();
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();

            //var ident = WindowsIdentity.GetCurrent();
            //var prin = new GenericPrincipal(ident, new string[] { EDISecurity.EDIADTMessageGenUser, EDISecurity.EDIORMMessageGenUser });
            //Thread.CurrentPrincipal = prin;
            Application.Current.MainWindow.Show();
        }

        protected override ILoggerFacade CreateLogger()
        {
            return new LoggerAdapter();
        }

    }
}
