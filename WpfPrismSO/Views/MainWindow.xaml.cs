using System.Windows;
using WpfPrismSO.ViewModels;

namespace WpfPrismSO
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(MainWindowViewModel view_model)
        {
            InitializeComponent();
            DataContext = view_model; //new MainWindowViewModel();
        }

        private void TextBox_Drop(object sender, DragEventArgs e)
        {
            //MessageBox.Show("Yeah, baby!!");
            var chachachar = this.DataContext as MainWindowViewModel;
            if (chachachar != null)
                chachachar.MVFieldToBindTo = "Fuck this shit!";
        }
    }
}
