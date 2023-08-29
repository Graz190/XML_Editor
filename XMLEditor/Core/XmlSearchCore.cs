using System;
using System.Collections.ObjectModel;
using System.Xml;
using XMLEditor.Pages;
using XMLEditor.Setting;

namespace XMLEditor.Core
{
    internal class XmlSearchCore
    {
        readonly MainWindow window;
        private readonly ObservableCollection<ValueID> listValueID;
        public XmlSearchCore(MainWindow mw)
        {
            window = mw;
            listValueID = new ObservableCollection<ValueID>();


        }

        internal ObservableCollection<ValueID> ListValueID => listValueID;

        public void BasicSearch(ViewModel.MainViewModel mainViewModel)
        {
            window.Dispatcher.BeginInvoke(new Action(() => { mainViewModel.DataGridItems.Clear(); }));

            var doc = new XmlDocument();
            var searchTerm = PropertySetting.Read_Setting(SettingsName.searchTerm);
            var searchTerm2 = PropertySetting.Read_Setting(SettingsName.searchTerm2);
            var loadText = "";
            window.Dispatcher.Invoke(() =>
            {
                loadText = PropertySetting.Read_Setting(SettingsName.FilePath);
            });
            doc.Load(loadText);

            var list = doc.GetElementsByTagName(searchTerm);
            var list2 = doc.GetElementsByTagName(searchTerm2);
        int counterID = 0;
            if (!string.IsNullOrEmpty(searchTerm2)) 
            {
                foreach (XmlNode node in list)
                {
                    if (node.InnerText != String.Empty)
                    {
                        window.Dispatcher.Invoke(() => { mainViewModel.DataGridItems.Add(new ValueID() { ID = counterID, Value = node.FirstChild.Value, Value2 = list2[counterID].FirstChild.Value }); });
                        counterID++;
                    }
                }
                window.Dispatcher.Invoke(() =>
                {
                    window.ChangeInformationText(ColorText.success, "Es wurden " + counterID + " mit dem Term " + searchTerm + " und " + searchTerm2 + " gefunden.");
                });
            }
            else
            {
                foreach (XmlNode node in list)
                {
                    if (node.InnerText != String.Empty)
                    {
                        window.Dispatcher.Invoke(() => { mainViewModel.DataGridItems.Add(new ValueID() { ID = counterID++, Value = node.FirstChild.Value, Value2 = "" }); });

                    }
                }
                window.Dispatcher.Invoke(() =>
                {
                    window.ChangeInformationText(ColorText.success, "Es wurden " + counterID + " mit dem Term " + searchTerm + " gefunden.");
                });
            }

        }
        public void IdSearch(ViewModel.MainViewModel mainViewModel)
        {
            window.Dispatcher.BeginInvoke(new Action(() => { mainViewModel.DataGridItems.Clear(); }));
            XmlDocument doc = new XmlDocument();
            String loadText = "";
            window.Dispatcher.Invoke(() =>
            {
                loadText = PropertySetting.Read_Setting(SettingsName.FilePath);
            });
            doc.Load(loadText);
            XmlNodeList list = doc.GetElementsByTagName(PropertySetting.Read_Setting(SettingsName.searchTerm));
            XmlNodeList list2 = doc.GetElementsByTagName(PropertySetting.Read_Setting(SettingsName.searchTerm2));
            if (PropertySetting.Read_Setting(SettingsName.searchTerm) != "")
            {
                XmlNode parent = null;
                foreach (XmlNode node in list)
                {
                    if (node.InnerText != String.Empty)
                    {
                        if (node.InnerText.Equals(PropertySetting.Read_Setting(SettingsName.IDName)))
                        {

                            parent = node.ParentNode;
                            break;
                        }
                    }
                }
                var selectedParentNode = parent.CreateNavigator();
                var selectedNode = selectedParentNode.SelectSingleNode(PropertySetting.Read_Setting(SettingsName.searchTerm));
                window.Dispatcher.Invoke(() => { mainViewModel.DataGridItems.Add(new ValueID() { ID = 1, Value = SettingsName.IDName, Value2 = selectedNode.InnerXml }); });

            }
        }
    }
}
