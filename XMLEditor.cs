using Internal;
using System.Reflection.Metadata;
using System.Xml.XPath;
using System.Xml;
class XMLEditor
{
    public boolean runReplacer()
    {
        XmlDocument doc = new XmlDocument();
        doc.LoadXML("C:\\Users\\NCTR Admin\\Desktop\\zieldatei.xml");
        XmlNode root = doc.DocumentElement;
        XPathNavigator navigator = doc.CreateNavigator();
        foreach (var nav in navigator.Select("GUID"))
        {
            nav.SetValue("");
            Console.WriteLine("test")
        }
        return true;
    }
    static void Main(string[] args)
    {
        XMLEditor editor = new XMLEditor();
        editor.runReplacer();
        
    }
}
