using System.Windows;

namespace WpfPrismSO
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            Dispatcher.UnhandledException += OnDispatcherUnhandledException;

            base.OnStartup(e);
            Bootstrapper bs = new Bootstrapper();
            bs.Run();
        }


        void OnDispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            string errorMessage = $"OUCH! An unhandled exception occurred: {e.Exception.Message}";
            MessageBox.Show(errorMessage, "This application might suck...", MessageBoxButton.OK, MessageBoxImage.Error);
            e.Handled = true;
        }

    }
}
