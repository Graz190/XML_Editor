using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using XMLEditor.Core;
using XMLEditor.Setting;

namespace XMLEditor.Pages
{
    /// <summary>
    /// Interaktionslogik für TransformerPage.xaml
    /// </summary>
    public partial class TransformerPage : Page
    {
        readonly BackgroundWorker worker1 = new BackgroundWorker();
        readonly XMLTransformer transformer;
        readonly MainWindow window;
        public TransformerPage(MainWindow window)
        {
            InitializeComponent();
            transformer = new XMLTransformer(window);
            this.window = window;
            worker1.DoWork += Worker1_DoWork;
            worker1.RunWorkerCompleted += Worker1_RunWorkerCompleted;
        }

        private void OpenFile(object sender, RoutedEventArgs e)
        {
            Button btn=sender as Button;
            String filter = "XML Dateien (*.xml)|*.xml";
            if (btn.Name.Equals("openFileButton2")) filter = "XSLT Dateien (*.xslt)|*.xslt";

            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
               Filter = filter
            };

            if (openFileDialog.ShowDialog() == true)
            {
                if (btn.Name.Equals("openFileButton"))
                {
                    openFilePathBox.Text = openFileDialog.FileName;
                    openFileButton2.Focus();
                }
                else
                {
                    openFilePathBox2.Text = openFileDialog.FileName;
                    StartButton.Focus();
                }
            }
        }
        private void RunTransformer(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(openFilePathBox.Text) || String.IsNullOrEmpty(openFilePathBox2.Text)){
                    window.ChangeInformationText(ColorText.error, "Bitte XSLT oder XML Pfad überprüfen.");
                return;
            }
            PropertySetting.Save_Setting(SettingsName.FilePath, openFilePathBox.Text);
            PropertySetting.Save_Setting(SettingsName.FilePath2, openFilePathBox2.Text);
            if (!worker1.IsBusy)
            {
                worker1.RunWorkerAsync();
            }

        }

        private void Worker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
            }
            else
            {
                window.ChangeInformationText(ColorText.success, "XML wurde erfolgreich in CSV umgewandelt.");
            }
        }

        private void Worker1_DoWork(object sender, DoWorkEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                window.ChangeInformationText(ColorText.loading, "Bitte warten Datei wird verarbeitet");
            });
            transformer.transformXMLToCSV();
        }

        private void ShutdownApp(object sender, RoutedEventArgs e) => Application.Current.Shutdown();
    }
}
