using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FastColoredTextBoxNS;
using System.Xml;
using System.IO;
using System.Drawing;
using System.Globalization;

namespace OlalaPascal
{
    
    class SettingStatus
    {
        //public static string  spath =Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData)+"\\";
        private static Dictionary<string, Style> highlightsetting;
        public static Font font { get; set; }
        public static string fileNameHighlight = null;
        private static readonly Dictionary<string, SyntaxDescriptor> descByXMLfileNames = new Dictionary<string, SyntaxDescriptor>();
        public static XmlDocument doc;
        // contructor
        public static Dictionary<string, Style> HighlightSetting
        {
            get { return highlightsetting; }
            set { highlightsetting = value; }
        }


        public SettingStatus()
        {
            highlightsetting = new Dictionary<string, Style>();

        }
    
        public static void GetHighlightOption()
        {
            if (!File.Exists(AppPath.Data+"EditorSetting.xml"))
            {

                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\Olala Pascal");
                File.WriteAllText(AppPath.Data+ "EditorSetting.xml", OlalaPascal.Properties.Resources.Light);
            }
            doc = new XmlDocument();
            doc.Load(AppPath.Data + "EditorSetting.xml");
            XmlNode xmlFont = doc.SelectSingleNode("HighlightSetting/Genaral");
            font = new Font(xmlFont.Attributes["fontName"].Value, (float)(Convert.ToDouble(xmlFont.Attributes["fontSize"].Value)));

        }


    }
}
