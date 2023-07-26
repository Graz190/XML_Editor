using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        ObservableCollection<ValueID> listValueID;
       public XmlSearchCore(MainWindow mw)
        {
            window = mw;
            listValueID = new ObservableCollection<ValueID>();
            
           
        }
        public void basicSearch(String searchTerm, ViewModel.MainViewModel mainViewModel)
        {
            window.Dispatcher.BeginInvoke(new Action(() => { mainViewModel.DataGridItems.Clear(); }));
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
                    window.Dispatcher.Invoke(() => { mainViewModel.DataGridItems.Add(new ValueID() { ID = counterID++, Value = node.FirstChild.Value }); });
                    
                }
            }
            window.Dispatcher.Invoke(() =>
            {
                window.changeInformationText(ColorText.success, "Es wurden " +counterID+ " mit dem Term "+PropertySetting.read_Setting(SettingsName.searchTerm) + " gefunden.");
            });
            
        }
    }
}
