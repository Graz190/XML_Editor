using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using XMLEditor.Core;
using XMLEditor.Setting;
using XMLEditor.ViewModel;

namespace XMLEditor
{
    /// <summary>
    /// Interaktionslogik für SearchPage.xaml
    /// </summary>
    public partial class SearchPage : Page
    {
        readonly BackgroundWorker worker1 = new BackgroundWorker();
        readonly MainWindow window;
        readonly XmlSearchCore searchCore;
        private MainViewModel mainViewModel;
        public SearchPage(MainWindow window)
        {
            InitializeComponent();
            worker1.DoWork += Worker1_DoWork;
            worker1.RunWorkerCompleted += Worker1_RunWorkerCompleted;
            this.window = window;
            searchCore = new XmlSearchCore(window);
            MainViewModel mainViewModel = new MainViewModel();
            DataContext = mainViewModel;
        }
        private void RunSearch(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(PropertySetting.Read_Setting(SettingsName.ResultFile)) && !string.IsNullOrEmpty(openFilePathBox.Text))
            {
                if (string.IsNullOrEmpty(searchTermBox.Text)) window.ChangeInformationText(ColorText.error, "Bitte geben sie einen gesuchten Term an");
                else
                {
                    PropertySetting.Save_Setting(SettingsName.FilePath, openFilePathBox.Text);
                    PropertySetting.Save_Setting(SettingsName.searchTerm, searchTermBox.Text);
                    PropertySetting.Save_Setting(SettingsName.searchTerm2, searchTermBox2.Text);
                    if (!worker1.IsBusy)
                    {
                        worker1.RunWorkerAsync();
                    }
                }
            }
            else
            {
                window.ChangeInformationText(ColorText.error, "Bitte wählen sie die Xml Datei aus");
            }
        }
        private void OpenFile(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                Filter = "XML Dateien (*.xml)|*.xml"
            };


            if (openFileDialog.ShowDialog() == true) openFilePathBox.Text = openFileDialog.FileName;
            StartButton.Focus();
        }
        private void ShutdownApp(object sender, RoutedEventArgs e) => Application.Current.Shutdown();

        private void Worker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
            }
        }

        private void Worker1_DoWork(object sender, DoWorkEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                searchTermLabel.Content = "";
                searchTermLabel.Content = PropertySetting.Read_Setting(SettingsName.searchTerm);
                window.ChangeInformationText(ColorText.loading, "Bitte warten Datei wird verarbeitet");
                mainViewModel = DataContext as MainViewModel;
            });
            searchCore.BasicSearch(mainViewModel);
            
        }

        private void SearchTermBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                e.Handled = true;
                StartButton.Focus();
            }
        }
    }
}
