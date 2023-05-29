using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSFSAutostartManager
{
    public class FindExeXml
    {
        private static string GetEXEXMLPathStore() => Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "/Packages/Microsoft.FlightSimulator_8wekyb3d8bbwe/LocalCache/EXE.xml";
        private static string GetEXEXMLPathSteam() => Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/Microsoft Flight Simulator/EXE.xml";

        private string _exeXmlPath = String.Empty;
        public string ExeXmlPath
        {
            get { return _exeXmlPath; }
            private set
            {
                Console.WriteLine($"EXE.XML Path: {value}");
                _exeXmlPath = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pathOverride"></param>
        /// <exception cref="DirectoryNotFoundException"></exception>
        public FindExeXml(string pathOverride = "")
        {
            if (!String.IsNullOrWhiteSpace(pathOverride))
            {
                if (Directory.Exists(pathOverride))
                {
                    ExeXmlPath = pathOverride;
                    return;
                }
                throw new DirectoryNotFoundException($"Could not fine EXE.xml at path: {pathOverride}");
            }

            if (Directory.Exists(Path.GetDirectoryName(GetEXEXMLPathStore())))
            {
                ExeXmlPath = GetEXEXMLPathStore();
                return;
            }

            if (Directory.Exists(Path.GetDirectoryName(GetEXEXMLPathSteam())))
            {
                ExeXmlPath = GetEXEXMLPathSteam();
                return;
            }

            throw new DirectoryNotFoundException("Could not find EXE.xml path");
        }
    }
}
