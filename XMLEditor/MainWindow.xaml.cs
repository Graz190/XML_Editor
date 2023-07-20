using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml;
using System.IO;
using System.Configuration;
using System.Threading;
using System.ComponentModel;

namespace XMLEditor
{

    /// <summary>
    /// Interaktionslogik für mw.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int counter=0;
        private XmlEditor editor;

        //DefaultSetting
        private String TARGETTAGNAME = "GUID";
        private String RESULTFILENAME = "Result";
        private String REPLACEDVALUE = "";
        public MainWindow()
        {
            InitializeComponent();
            editor = new XmlEditor(this);
            
            worker1.DoWork += Worker1_DoWork;
            worker1.RunWorkerCompleted += Worker1_RunWorkerCompleted;
            save_Setting("ResultFileName", RESULTFILENAME);
            save_Setting("TargetTagName", TARGETTAGNAME);
            save_Setting("ReplacedValue", REPLACEDVALUE);
        }
        BackgroundWorker worker1 = new BackgroundWorker();
        private void runEditor(object sender, RoutedEventArgs e)
        {
            this.StartButton.IsEnabled = false;

            if (editor.read_Setting("ResultFileName")!=""){
                if (this.openFilePathBox.Text=="")
                {
                    this.informationfield.TextAlignment = TextAlignment.Center;
                    this.informationfield.Foreground = Brushes.Red;
                    this.informationfield.Text = "Bitte wählen sie die Xml Datei aus";
                }
                else
                {
                    if (!worker1.IsBusy)
                    {
                        worker1.RunWorkerAsync();
                    }
                }
            }
            else
            {
                    this.informationfield.TextAlignment = TextAlignment.Center;
                    this.informationfield.Foreground = Brushes.Red;
                    this.informationfield.Text = "Bitte wählen sie die Xml Datei aus";
            }
            this.StartButton.IsEnabled = true;
        }

        private void Worker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if(e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
            }

        }

        private void Worker1_DoWork(object sender, DoWorkEventArgs e)
        {
            this.Dispatcher.Invoke(() => {
                this.informationfield.TextAlignment = TextAlignment.Center;
                this.informationfield.Foreground = Brushes.DarkOrange;
                this.informationfield.Text = "Bitte warten Datei wird verarbeitet";
            });
            editor.runReplacer();
  

        }

        private void openFile(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            openFileDialog.Filter = "XML Dateien (*.xml)|*.xml";

            if (openFileDialog.ShowDialog() == true)openFilePathBox.Text =  openFileDialog.FileName;
            
        }
        private void shutdownApp(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Control ctrl = sender as Control;

            if(ctrl!= null)
            {
                String name = ctrl.Name;
                if(name == "Einstellung")
                {
                    SettingsWindow win1 = new SettingsWindow();
                    win1.ShowDialog();
                }
                else if(name == "Credit")
                {
                    MessageBox.Show("XML Editor Version 0.0.4\n\nMade with Love by Christian Fagherazzi", "Credits");
                }
            }
        }
        private void save_Setting(string setting_Name, string setting_Value)
        {
            string property_name = setting_Name;

            SettingsProperty prop = null;
            if (Properties.Settings.Default.Properties[property_name] != null)
            {
                prop = Properties.Settings.Default.Properties[property_name];
            }
            else
            {
                prop = new SettingsProperty(property_name);
                prop.PropertyType = typeof(string);
                Properties.Settings.Default.Properties.Add(prop);
                Properties.Settings.Default.Save();
            }
            Properties.Settings.Default.Properties[property_name].DefaultValue = setting_Value;

            Properties.Settings.Default.Save();
        }
    }

    internal class XmlEditor
    {
        public XmlEditor(MainWindow mainWindow)
        {
            mw = mainWindow;
        }


        public MainWindow mw { get; }
        public int Counter { get; set; }

        public bool runReplacer()
        {

                XmlDocument doc = new XmlDocument();
            String loadText = "";
            mw.Dispatcher.Invoke(() =>
            {
                loadText = mw.openFilePathBox.Text;
            });
            doc.Load(loadText);
            XmlNodeList list = doc.GetElementsByTagName(read_Setting("TargetTagName"));
                Counter = 0;

                foreach (XmlNode node in list)
                {
                    if(node.InnerText !=String.Empty)
                    {
                        node.FirstChild.Value = read_Setting("ReplacedValue");
                        Counter++;
                    }
                }
            FileInfo currentFile = new FileInfo(loadText);

                doc.Save(currentFile.Directory.FullName + "\\" + read_Setting("ResultFileName") + currentFile.Extension);
                

                string[] lines = File.ReadAllLines(currentFile.Directory.FullName + "\\" + read_Setting("ResultFileName") + currentFile.Extension);
                File.WriteAllLines(currentFile.Directory.FullName + "\\" + read_Setting("ResultFileName") + currentFile.Extension, lines);

                mw.Dispatcher.Invoke(() =>
                {
                mw.informationfield.TextAlignment = TextAlignment.Center;
                mw.informationfield.Foreground = Brushes.Green;
                mw.informationfield.Text = "Der Inhalt von " + read_Setting("TargetTagName") + " wurde erfolgreich entfernt. \n" + "Es wurden " + Counter + " Inhalte von " + read_Setting("TargetTagName") + " entfernt.";
                    
            });
                return true;
        }
        public string read_Setting(string setting_Name) {
            string sResult = "";
            if (Properties.Settings.Default.Properties[setting_Name] != null)
            {
                sResult = Properties.Settings.Default.Properties[setting_Name].DefaultValue.ToString();
            }
            if (sResult == "NaN") sResult = "0";

            return sResult;
        }
    }
}
