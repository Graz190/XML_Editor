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
using XMLEditor.Setting;

namespace XMLEditor
{

    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        //DefaultSetting
        private String TARGETTAGNAME = "GUID";
        private String RESULTFILENAME = "Result";
        private String REPLACEDVALUE = "";
        private Page rp;
        private Page sp;
        public MainWindow()
        {
            InitializeComponent();
            PropertySetting.save_Setting(SettingsName.ResultFile, RESULTFILENAME);
            PropertySetting.save_Setting(SettingsName.TargetTag, TARGETTAGNAME);
            PropertySetting.save_Setting(SettingsName.Replaced, REPLACEDVALUE);
            rp = new ReplacePage(this);
            sp = new SearchPage(this);
            Main.Content = rp;

        }

        private void shutdownApp(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Control ctrl = sender as Control;

            if (ctrl != null)
            {
                String name = ctrl.Name;
                if (name == "Einstellung")
                {
                    SettingsWindow win1 = new SettingsWindow();
                    win1.ShowDialog();
                }
                else if (name == "Credit")
                {
                    MessageBox.Show("XML Editor Version 0.0.6\n\nMade with Love by Christian Fagherazzi", "Credits");
                }
                else if (name == "Suche")
                {
                    changeInformationText(ColorText.defaultColor, "");
                    if(Main.Content == rp)
                    {
                        Main.Content = sp;
                    }
                    else
                    {
                        Main.Content = rp;
                    }
                }
            }
        }
        /// <summary>
        /// Change Information Text
        /// </summary>
        /// <param name="brush">Color of the font</param>
        /// <param name="text">Displayed Text</param>
        public void changeInformationText(SolidColorBrush brush,String text)
        {
            this.informationfield.TextAlignment = TextAlignment.Center;
            this.informationfield.Foreground = brush;
            this.informationfield.Text = text;
        }
    }

}
