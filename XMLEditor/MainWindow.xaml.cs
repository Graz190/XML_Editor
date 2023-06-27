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
            if (!editor.runReplacer())
            {
                this.informationfield.TextAlignment = TextAlignment.Center;
                this.informationfield.Foreground = Brushes.Red;
                this.informationfield.Text = "Bitte wählen sie die Xml Datei aus";
            }
        }
        private void openFile(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            openFileDialog.Filter = "XML Dateien (*.xml)|*.xml";

            if (openFileDialog.ShowDialog() == true)openFilePathBox.Text = openFileDialog.FileName;
            
        }
        private void shutdownApp(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
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
                doc.Save(mw.openFilePathBox.Text);

                mw.informationfield.TextAlignment = TextAlignment.Center;
                mw.informationfield.Foreground = Brushes.Green;
                mw.informationfield.Text = "Der Inhalt von GUID wurde erfolgreich entfernt. \n" + "Es wurden " + Counter + " Inhalte von GUID entfernt.";

                return true;
            }
            return false;
        }
    }
}
