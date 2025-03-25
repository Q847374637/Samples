using ProjectSettings.Entities.SettingsEntities;
using Samples.Entities.SampleActionEntities;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Samples.UI.MVVM.View
{
    /// <summary>
    /// Логика взаимодействия для HomeView.xaml
    /// </summary>
    public partial class HomeView : UserControl
    {
        public HomeView()
        {
            InitializeComponent();
        }

        public string SelectedValue { get; private set; }

        private void button_Click(object sender, RoutedEventArgs e)
        {


            Thread delayThread = new Thread(DelaySample);
            Thread easypayThread = new Thread(EasypaySample);

            delayThread.Name = nameof(DelaySample);
            easypayThread.Name = nameof(EasypaySample);

            switch (SelectedValue)
            {
                case "Delay & Easypay":
                    delayThread.Start();
                    easypayThread.Start();
                    break;
                case "Delay":
                    delayThread.Start();
                    break;
                case "Easypay":
                    easypayThread.Start();
                    break;
            }
        }

        void DelaySample()
        {
            SampleInstance sampleInstanceDelay = new SampleInstance();
            sampleInstanceDelay.SampleRun(DBases.DBasesDelay, Extensions.Extension, Pathes.PathCopyDelay, Pathes.PathGetDelay, 0, Columns.ColumnsDelay, Columns.FinalColumnsDelay);
            MessageBox.Show("Создание выборки Delay завершён.");
        }

        void EasypaySample()
        {
            SampleInstance sampleInstanceEasypay = new SampleInstance();
            sampleInstanceEasypay.SampleRun(DBases.DbasesEasypay, Extensions.Extension, Pathes.PathCopyEasypay, Pathes.PathGetEasypay, 1, Columns.ColumnsEasypay, Columns.FinalColumnsEasypay);
            MessageBox.Show("Процесс создания выборки Easypay завершён.");
        }

        private void radioButtonDE_Checked(object sender, RoutedEventArgs e)
        {
            memorize_Selection(sender, e);
        }

        private void radioButtonD_Checked(object sender, RoutedEventArgs e)
        {
            memorize_Selection(sender, e);
        }

        private void radioButtonE_Checked(object sender, RoutedEventArgs e)
        {
            memorize_Selection(sender, e);
        }

        public void memorize_Selection(object sender, RoutedEventArgs e)
        {
            RadioButton pressed = (RadioButton)sender;
            SelectedValue = pressed.Content.ToString();
        }
    }
}
