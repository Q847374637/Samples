using System.Linq;
using System.Windows;
using System.Windows.Controls;
using SettingsEntitiesPathes = ProjectSettings.Entities.SettingsEntities.Pathes;

namespace Samples.UI.MVVM.View
{
    /// <summary>
    /// Логика взаимодействия для SettingsView.xaml
    /// </summary>
    public partial class SettingsView : UserControl
    {
        public SettingsView()
        {
            InitializeComponent();
            LoadTextboxData();
        }

        private void LoadTextboxData()
        {
            textboxDelayInInitialize();
            textboxDelayOutInitialize();
            textboxEasypayInInitialize();
            textboxEasypayOutInitialize();
        }

        private void buttonSetDefaultDelay_Click(object sender, RoutedEventArgs e)
        {
            SettingsEntitiesPathes.SetDefaultPathGetDelay();
            LoadTextboxData();
        }

        private void buttonSetDefaultEasypay_Click(object sender, RoutedEventArgs e)
        {
            SettingsEntitiesPathes.SetDefaultPathGetEasypay();
            LoadTextboxData();
        }

        private void textboxDelayInInitialize()
        {
            textboxDelayIn.Text = SettingsEntitiesPathes.PathGetDelay.First();
        }

        private void textboxDelayOutInitialize()
        {
            textboxDelayOut.Text = SettingsEntitiesPathes.PathGetDelay.Last();
        }

        private void textboxEasypayInInitialize()
        {
            textboxEasypayIn.Text = SettingsEntitiesPathes.PathGetEasypay.First();
        }

        private void textboxEasypayOutInitialize()
        {
            textboxEasypayOut.Text = SettingsEntitiesPathes.PathGetEasypay.Last();
        }

        private void textboxDelayInSave()
        {
            SettingsEntitiesPathes.ModifyPathCopyDelay(0, textboxDelayIn.Text);
        }

        private void textboxDelayOutSave()
        {
            SettingsEntitiesPathes.ModifyPathCopyDelay(1, textboxDelayOut.Text);
        }

        private void textboxEasypayInSave()
        {
            SettingsEntitiesPathes.ModifyPathCopyEasypay(0, textboxEasypayIn.Text);
        }

        private void textboxEasypayOutSave()
        {
            SettingsEntitiesPathes.ModifyPathCopyEasypay(1, textboxEasypayOut.Text);
        }

        private void buttonSaveSettings_Click(object sender, RoutedEventArgs e)
        {
            textboxDelayInSave();
            textboxDelayOutSave();
            textboxEasypayInSave();
            textboxEasypayOutSave();
            LoadTextboxData();
        }
    }
}
