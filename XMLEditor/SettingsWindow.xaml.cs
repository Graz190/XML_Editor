using System;
using System.Collections.Generic;
using System.Configuration;
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
using System.Windows.Shapes;

namespace XMLEditor
{
    /// <summary>
    /// Interaktionslogik für SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();
        }

        private void saveAllSetting(object sender, RoutedEventArgs e)
        {
            save_Setting("ResultFileName",this.resultName.Text);
            save_Setting("TargetTagName", this.tagName.Text);
            save_Setting("ReplacedValue", this.replacedName.Text);
            this.saveInformation.TextAlignment = TextAlignment.Center;
            this.saveInformation.Foreground = Brushes.Green;
            this.saveInformation.Text = "Einstellung wurden erfolgreich gespeichert";
        }

        private void cancel(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void save_Setting(string setting_Name, string setting_Value)
        {
            string property_name = setting_Name;

            SettingsProperty prop = null;
            if (Properties.Settings.Default.Properties[property_name]!= null)
            {
                prop = Properties.Settings.Default.Properties[property_name];
            }
            else
            {
                prop = new System.Configuration.SettingsProperty(property_name);
                prop.PropertyType= typeof(string);
                Properties.Settings.Default.Properties.Add(prop);
                Properties.Settings.Default.Save();
            }
            Properties.Settings.Default.Properties[property_name].DefaultValue= setting_Value;
            
            Properties.Settings.Default.Save();
        }
    }
}
