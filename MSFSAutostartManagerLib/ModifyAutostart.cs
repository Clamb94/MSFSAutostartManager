using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MSFSAutostartManager
{
    public class ModifyAutostart
    {
        public string XmlPath { get; private set; }
        public string AddonName { get; private set; }

        public ModifyAutostart(string addonName, string xmlPath = "")
        {

            FindExeXml finder = new FindExeXml(xmlPath);
            XmlPath = finder.ExeXmlPath;

            AddonName = addonName;

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            VerifyFilePathValid(XmlPath);
        }

        public void VerifyFilePathValid(string filePath)
        {
            if (String.IsNullOrWhiteSpace(XmlPath))
            {
                throw new ArgumentNullException(XmlPath);
            }
            if (!Directory.Exists(Path.GetDirectoryName(XmlPath)))
            {
                throw new DirectoryNotFoundException(XmlPath);
            }
        }

        public void Add(AddOptions opts)
        {

            XmlDocument doc = new XmlDocument();

            var context = new XmlParserContext(null, null, null, XmlSpace.None);
            context.Encoding = Encoding.GetEncoding(1252);

            if (File.Exists(XmlPath))
            {
                using (var r = XmlReader.Create(XmlPath, null, context))
                {
                    Console.WriteLine("Found existing EXE.xml");
                    doc.Load(r);
                }
            }
            else
            {
                using (var r = XmlReader.Create(new StringReader(EmptyXML.emptyContent), null, context))
                {
                    Console.WriteLine("No existing EXE.xml found. Generating new.");
                    doc.Load(r);
                }
            }

            XmlNode? node = doc.SelectSingleNode($"//Launch.Addon[Name='{AddonName}']");
            if (node != null)
            {
                Console.WriteLine("Autostart entry already exists. Removing it for now.");
                node.ParentNode?.RemoveChild(node);
            }

            XmlNode? simBaseNode = doc.SelectSingleNode("SimBase.Document");

            if (simBaseNode == null)
            {
                throw new ArgumentNullException(nameof(simBaseNode));
            }

            XmlElement addonElement = doc.CreateElement("Launch.Addon");
            XmlElement disabledElement = doc.CreateElement("Disabled");
            disabledElement.InnerText = opts.Disabled.ToString();
            addonElement.AppendChild(disabledElement);
            XmlElement manualLoadElement = doc.CreateElement("ManualLoad");
            manualLoadElement.InnerText = "False";
            addonElement.AppendChild(manualLoadElement);
            XmlElement nameElement = doc.CreateElement("Name");
            nameElement.InnerText = AddonName;
            addonElement.AppendChild(nameElement);
            XmlElement pathElement = doc.CreateElement("Path");
            pathElement.InnerText = opts.Path;
            addonElement.AppendChild(pathElement);
            XmlElement commandLineElement = doc.CreateElement("CommandLine");
            commandLineElement.InnerText = opts.CommandLine;
            addonElement.AppendChild(commandLineElement);
            XmlElement newConsoleElement = doc.CreateElement("NewConsole");
            newConsoleElement.InnerText = "False";
            addonElement.AppendChild(newConsoleElement);

            simBaseNode.AppendChild(addonElement);

            Console.WriteLine($"Entry added. Saving file.");
            doc.Save(XmlPath);
        }

        public void Remove()
        {
            if (!File.Exists(XmlPath))
            {
                return;
            }

            XmlDocument doc = new XmlDocument();

            var context = new XmlParserContext(null, null, null, XmlSpace.None);
            context.Encoding = Encoding.GetEncoding(1252);

            using (var r = XmlReader.Create(XmlPath, null, context))
            {
                doc.Load(r);
            }

            XmlNode? node = doc.SelectSingleNode($"//Launch.Addon[Name='{AddonName}']");
            if (node == null)
            {
                Console.WriteLine($"No entry found with <Name>{AddonName}</Name>");

            }

            node?.ParentNode?.RemoveChild(node);
            Console.WriteLine("Entry removed. Saving file.");
            doc.Save(XmlPath);
        }

        public bool CheckEnabled()
        {

            if (!File.Exists(XmlPath))
            {
                Console.WriteLine($"EXE.xml does not exist");
                return false;
            }

            XmlDocument doc = new XmlDocument();

            var context = new XmlParserContext(null, null, null, XmlSpace.None);
            context.Encoding = Encoding.GetEncoding(1252);

            using (var r = XmlReader.Create(XmlPath, null, context))
            {
                doc.Load(r);
            }

            XmlNode? node = doc.SelectSingleNode($"//Launch.Addon[Name='{AddonName}']");
            if (node != null)
            {
                //Check for <Disabled> node
                var v = node.SelectSingleNode("Disabled");
                if (v == null)
                {
                    Console.WriteLine("No <Disabled> node found. Considering enabled");
                    return true;
                }
                else
                {
                    return !bool.Parse(v.InnerText);
                }
            }
            else
            {
                //No <Launch.Addon> with the correct <Name> exists
                return false;
            }
        }
    }
}
