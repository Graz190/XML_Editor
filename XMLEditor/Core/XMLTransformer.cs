using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;
using System.Xml.Xsl;
using XMLEditor.Setting;

namespace XMLEditor.Core
{
    internal class XMLTransformer
    {
        readonly MainWindow window;
        public XMLTransformer(MainWindow mw) {
            window = mw;
        }
        public void transformXMLToCSV()
        {
            var doc = new XmlDocument();
            var loadXmlText = "";
            var loadCsvText = "";

            window.Dispatcher.Invoke(() =>
            {
                loadXmlText = PropertySetting.Read_Setting(SettingsName.FilePath);
                loadCsvText = PropertySetting.Read_Setting(SettingsName.FilePath2);
            });
            doc.Load(loadXmlText);
            XslCompiledTransform xslt = new XslCompiledTransform();
            xslt.Load(loadCsvText);
            FileInfo currentFile = new FileInfo(loadXmlText);

            String csvPath=currentFile.Directory.FullName + "\\" + PropertySetting.Read_Setting(SettingsName.ResultFile) + ".csv";

            using (XmlWriter writer = XmlWriter.Create(csvPath, xslt.OutputSettings))
            {
                xslt.Transform(doc,null,writer);
            }
        }
    }
}
