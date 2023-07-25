using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xml;
using XMLEditor.Pages;
using XMLEditor.Setting;

namespace XMLEditor.Core
{
    internal class XmlSearchCore
    {
        MainWindow window;
        List<ValueID> listValueID;
       public XmlSearchCore(MainWindow mw)
        {
            window = mw;
            listValueID = new List<ValueID>();
           
        }
        public List<ValueID> basicSearch(String searchTerm)
        {
            XmlDocument doc = new XmlDocument();
            String loadText = "";
            window.Dispatcher.Invoke(() =>
            {
                loadText = PropertySetting.read_Setting(SettingsName.FilePath);
            });
            doc.Load(loadText);
            XmlNodeList list = doc.GetElementsByTagName(PropertySetting.read_Setting(SettingsName.searchTerm));
            int counterID = 0;
            foreach (XmlNode node in list)
            {
                if (node.InnerText != String.Empty)
                {
                    listValueID.Add(new ValueID() { Id=counterID++, Value=node.FirstChild.Value });
                }
            }
            return listValueID;
            
        }
    }
}
