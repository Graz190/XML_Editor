using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
using System.Xml.XPath;
using System.Xml.Linq;
using System.Reflection;
using System.IO;
using System.Configuration;

namespace XMLEditor
{

    /// <summary>
    /// Interaktionslogik für mw.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int counter=0;
        private XmlEditor editor;
        public MainWindow()
        {
            InitializeComponent();
            editor = new XmlEditor(this);
        }
        private void runEditor(object sender, RoutedEventArgs e)
        {
            if (editor.read_Setting("ResultFileName")!=""){
                if (!editor.runReplacer())
                {
                    this.informationfield.TextAlignment = TextAlignment.Center;
                    this.informationfield.Foreground = Brushes.Red;
                    this.informationfield.Text = "Bitte wählen sie die Xml Datei aus";
                }
            }
            else
            {
                save_Setting("ResultFileName", "Result");
                if (!editor.runReplacer())
                {
                    this.informationfield.TextAlignment = TextAlignment.Center;
                    this.informationfield.Foreground = Brushes.Red;
                    this.informationfield.Text = "Bitte wählen sie die Xml Datei aus";
                }
            }
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
            SettingsWindow win1 = new SettingsWindow();
            win1.ShowDialog();
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
                prop = new System.Configuration.SettingsProperty(property_name);
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
        readonly string TARGETNODE = "GUID";
        public XmlEditor(MainWindow mainWindow)
        {
            mw = mainWindow;
        }


        public MainWindow mw { get; }
        public int Counter { get; set; }
        public Boolean runReplacer()
        {
            
            if (mw.openFilePathBox.Text != "")
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(mw.openFilePathBox.Text);

                XmlNodeList list = doc.GetElementsByTagName(TARGETNODE);
                Counter = 0;

                foreach (XmlNode node in list)
                {
                    if(node.InnerText !=String.Empty)
                    {
                        node.FirstChild.Value = "";
                        Counter++;
                    }
                }
                FileInfo currentFile = new FileInfo(mw.openFilePathBox.Text);
                doc.Save(currentFile.Directory.FullName + "\\" + read_Setting("ResultFileName") + currentFile.Extension);
                
                //string[] lines = File.ReadAllLines(currentFile.Directory.FullName + "\\" + "result" + currentFile.Extension);
                //File.WriteAllLines(currentFile.Directory.FullName + "\\" + "result" + currentFile.Extension, lines);

                string[] lines = File.ReadAllLines(currentFile.Directory.FullName + "\\" + read_Setting("ResultFileName") + currentFile.Extension);
                File.WriteAllLines(currentFile.Directory.FullName + "\\" + read_Setting("ResultFileName") + currentFile.Extension, lines);

                mw.informationfield.TextAlignment = TextAlignment.Center;
                mw.informationfield.Foreground = Brushes.Green;
                mw.informationfield.Text = "Der Inhalt von GUID wurde erfolgreich entfernt. \n" + "Es wurden " + Counter + " Inhalte von GUID entfernt.";

                return true;
            }
            return false;
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
