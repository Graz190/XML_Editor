using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml;
using XMLEditor.Setting;

namespace XMLEditor
{
    internal class XmlEditorCore
    {
        public XmlEditorCore(MainWindow mainWindow)
        {
            mw = mainWindow;
        }


        public MainWindow mw { get; }
        public int Counter { get; set; }
        public void runSearchValue(String searchTag)
        {

        }
        public void runReplacer()
        {
            int caseNumber = 1;
            switch (caseNumber)
            {
                case 1:
                    XmlDocument doc = new XmlDocument();
                    String loadText = "";
                    mw.Dispatcher.Invoke(() =>
                    {
                        loadText = PropertySetting.Read_Setting(SettingsName.FilePath);
                    });
                    doc.Load(loadText);
                    XmlNodeList list = doc.GetElementsByTagName(PropertySetting.Read_Setting(SettingsName.TargetTag));
                    Counter = 0;

                    foreach (XmlNode node in list)
                    {
                        if (node.InnerText != String.Empty)
                        {
                            node.FirstChild.Value = PropertySetting.Read_Setting(SettingsName.Replaced);
                            Counter++;
                        }
                    }
                    FileInfo currentFile = new FileInfo(loadText);

                    doc.Save(currentFile.Directory.FullName + "\\" + PropertySetting.Read_Setting(SettingsName.ResultFile) + currentFile.Extension);


                    string[] lines = File.ReadAllLines(currentFile.Directory.FullName + "\\" + PropertySetting.Read_Setting(SettingsName.ResultFile) + currentFile.Extension);
                    File.WriteAllLines(currentFile.Directory.FullName + "\\" + PropertySetting.Read_Setting(SettingsName.ResultFile) + currentFile.Extension, lines);

                    mw.Dispatcher.Invoke(() =>
                    {
                        mw.ChangeInformationText(ColorText.success, "Der Inhalt von " + PropertySetting.Read_Setting(SettingsName.TargetTag) + " wurde erfolgreich entfernt. \n" + "Es wurden " + Counter + " Inhalte von " + PropertySetting.Read_Setting(SettingsName.TargetTag) + " entfernt.");
                    });
                    break;
            }
        }
    }
}
