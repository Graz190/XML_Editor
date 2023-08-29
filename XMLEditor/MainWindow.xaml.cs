using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;
using System.Xml;
using XMLEditor.Pages;
using XMLEditor.Setting;
using XMLEditor.ViewModel;

namespace XMLEditor
{

    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        //DefaultSetting
        private readonly string TARGETTAGNAME = "GUID";
        private readonly string RESULTFILENAME = "Result";
        private readonly string REPLACEDVALUE = "";
        private readonly Page rp;
        private readonly Page sp;
        private readonly Page tp;
        public MainWindow()
        {
            InitializeComponent();
            PropertySetting.Save_Setting(SettingsName.ResultFile, RESULTFILENAME);
            PropertySetting.Save_Setting(SettingsName.TargetTag, TARGETTAGNAME);
            PropertySetting.Save_Setting(SettingsName.Replaced, REPLACEDVALUE);
            rp = new ReplacePage(this);
            sp = new SearchPage(this);
            tp = new TransformerPage(this);
            Main.Content = rp;
            DataContext = new MainViewModel();
        }

        private void ShutdownApp(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (!(sender is Control ctrl))
            {
                return;
            }
            String name = ctrl.Name;
            if (name == "Einstellung")
            {
                SettingsWindow win1 = new SettingsWindow();
                win1.ShowDialog();
            }
            else if (name == "Credit")
            {
                MessageBox.Show("XML Editor Version 0.1.0\n\nMade with Love by Christian Fagherazzi", "Credits");
            }
            else if (name == "Suche")
            {
                ChangeInformationText(ColorText.defaultColor, "");
                if (Main.Content != sp)
                {
                    Main.Content = sp;
                }
                else
                {
                    Main.Content = rp;
                }
            }
            else if (name == "CSVTransformer")
            {
                ChangeInformationText(ColorText.defaultColor, "");
                if (Main.Content != tp)
                {
                    Main.Content = tp;
                }
                else
                {
                    Main.Content = rp;
                }
            }
        }
        /// <summary>
        /// Change Information Text
        /// </summary>
        /// <param name="brush">Color of the font</param>
        /// <param name="text">Displayed Text</param>
        public void ChangeInformationText(SolidColorBrush brush, string text)
        {
            this.informationfield.TextAlignment = TextAlignment.Center;
            this.informationfield.Foreground = brush;
            this.informationfield.Text = text;
        }
    }

}
