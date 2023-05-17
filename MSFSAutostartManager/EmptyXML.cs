using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSFSAutostartManager
{
    static class EmptyXML
    {
        public static readonly string emptyContent = @"<?xml version=""1.0"" encoding=""Windows-1252""?>
            <SimBase.Document Type=""Launch"" version=""1,0"">
              <Descr>Launch</Descr>
              <Filename>EXE.xml</Filename>
              <Disabled>False</Disabled>
              <Launch.ManualLoad>False</Launch.ManualLoad>
            </SimBase.Document>";
    }
}
