using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using XMLEditor.Setting;

namespace XMLEditor
{

    /// <summary>
    /// Interaktionslogik für ReplacePage.xaml
    /// </summary>
    public partial class ReplacePage : Page
    {
        private readonly XmlEditorCore editor;
        private readonly MainWindow window;
        public ReplacePage(MainWindow mw)
        {
            InitializeComponent();
            window = mw;
            editor = new XmlEditorCore(window);

            worker1.DoWork += Worker1_DoWork;
            worker1.RunWorkerCompleted += Worker1_RunWorkerCompleted;
        }

        private readonly BackgroundWorker worker1 = new BackgroundWorker();

        public int Counter { get; set; } = 0;

        private void RunEditor(object sender, RoutedEventArgs e)
        {
            if (PropertySetting.Read_Setting(SettingsName.ResultFile) != "")
            {
                if (this.openFilePathBox.Text == "")
                {
                    window.ChangeInformationText(ColorText.error, "Bitte wählen sie die Xml Datei aus");
                }
                else
                {
                    PropertySetting.Save_Setting(SettingsName.FilePath, openFilePathBox.Text);
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

        private void Worker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
            }
        }

        private void Worker1_DoWork(object sender, DoWorkEventArgs e)
        {
            this.Dispatcher.Invoke(() =>
            {
                window.ChangeInformationText(ColorText.loading, "Bitte warten Datei wird verarbeitet");
            });
            editor.runReplacer();
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
        private void ShutdownApp(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

    }
}


