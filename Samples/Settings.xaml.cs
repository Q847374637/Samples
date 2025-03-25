using System.Windows;
using System.Windows.Controls;


namespace Samples
{
    public partial class Settings : Window
    {
        public string SelectedValue { get; private set; }

        public Settings()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
     
            
        }

        private void pathesDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
