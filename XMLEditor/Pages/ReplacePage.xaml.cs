using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
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
using System.Xml;
using XMLEditor;
using XMLEditor.Setting;

namespace XMLEditor
{

    /// <summary>
    /// Interaktionslogik für ReplacePage.xaml
    /// </summary>
    public partial class ReplacePage : Page
    {
        public int counter = 0;
        private XmlEditorCore editor;
        private MainWindow window;
        public ReplacePage(MainWindow mw)
        {
            InitializeComponent();
            window = mw;
            editor = new XmlEditorCore(window);

            worker1.DoWork += Worker1_DoWork;
            worker1.RunWorkerCompleted += Worker1_RunWorkerCompleted;

        }
        BackgroundWorker worker1 = new BackgroundWorker();


        private void runEditor(object sender, RoutedEventArgs e)
        {
            if (PropertySetting.read_Setting(SettingsName.ResultFile) != "")
            {
                if (this.openFilePathBox.Text == "")
                {
                    window.changeInformationText(ColorText.error, "Bitte wählen sie die Xml Datei aus");
                }
                else
                {
                    PropertySetting.save_Setting(SettingsName.FilePath, this.openFilePathBox.Text);
                    if (!worker1.IsBusy)
                    {
                        worker1.RunWorkerAsync();
                    }
                }
            }
            else
            {
                window.changeInformationText(ColorText.error, "Bitte wählen sie die Xml Datei aus");
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
                window.changeInformationText(ColorText.loading, "Bitte warten Datei wird verarbeitet");
            });
            editor.runReplacer();
        }

        private void openFile(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            openFileDialog.Filter = "XML Dateien (*.xml)|*.xml";

            if (openFileDialog.ShowDialog() == true) openFilePathBox.Text = openFileDialog.FileName;

        }
        private void shutdownApp(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

    }
}


