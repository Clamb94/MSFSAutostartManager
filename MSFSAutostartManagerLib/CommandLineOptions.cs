using CommandLine.Text;
using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSFSAutostartManager
{
    [Verb("add", HelpText = "Add file contents to the index.")]
    public class AddOptions
    {
        [Option('n', "name", Required = true, HelpText = "<Name> of the addon to be launched. Case sensitive!")]
        public string Name { get; set; } = null!;

        [Option('p', "path", Required = true, HelpText = "<Path> to the file to be opened")]
        public string Path { get; set; } = null!;

        //If the command line arguments start with an "-" (like: -auto), an escape "\" has to be added in front
        [Option('c', "commandline", Required = false, HelpText = "(optional) <CommandLine> arguments")]
        public string CommandLine
        {
            get { return _commandLine; }
            set
            {                
                if (value[..1].Equals("\\")) { value = value[1..]; }
                _commandLine = value;
            }
        }
        private string _commandLine = String.Empty;

        [Option('d', "disabled", Required = false, HelpText = "(optional) <disabled> true/false")]
        public bool Disabled { get; set; } = false;

        [Option('x', "xmlpath", Required = false, HelpText = "(optional) manually set location of EXE.xml")]
        public string XmlPath { get; set; } = String.Empty;

    }
    [Verb("remove", HelpText = "Record changes to the repository.")]
    public class RemoveOptions
    {
        [Option('n', "name", Required = true, HelpText = "<Name> of the addon to be launched")]
        public string Name { get; set; } = null!;

        [Option('x', "xmlpath", Required = false, HelpText = "(optional) manually set location of EXE.xml")]
        public string XmlPath { get; set; } = String.Empty;
    }
    [Verb("checkEnabled", HelpText = "Clone a repository into a new directory.")]
    public class IsEnabledOptions
    {
        [Option('n', "name", Required = true, HelpText = "<Name> of the addon to be launched")]
        public string Name { get; set; } = null!;

        [Option('x', "xmlpath", Required = false, HelpText = "(optional) manually set location of EXE.xml")]
        public string XmlPath { get; set; } = String.Empty;
    }
}
