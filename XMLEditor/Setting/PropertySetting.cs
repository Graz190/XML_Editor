using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMLEditor.Setting
{
    internal class PropertySetting
    {
        public static void save_Setting(string setting_Name, string setting_Value)
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
        public static string read_Setting(string setting_Name)
        {
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
