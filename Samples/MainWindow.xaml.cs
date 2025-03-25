using System.Windows;


namespace Samples
{
    public partial class MainWindow : Window
    {      
        public MainWindow()
        {
            InitializeComponent();
        }
        private void minimizeApplication(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        private void closeApplication(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
